using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Lawyeed.API.Lawyeed.Domain.Models;
using Lawyeed.API.Lawyeed.Domain.Services;
using Lawyeed.API.Lawyeed.Resources;
using Lawyeed.API.Lawyeed.Services;
using Lawyeed.API.Shared.Extensions;

namespace Lawyeed.API.Lawyeed.Controllers;


[Route("/api/v1/[controller]")]
[ApiController]
public class PersonPlanController : ControllerBase
{
    private readonly IPersonPlanService _personPlanService;
    private readonly IMapper _mapper;

    public PersonPlanController(IPersonPlanService personPlanService, IMapper mapper)
    {
        _personPlanService = personPlanService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<PersonPlanResource>> GetAllAsync()
    {
        var personPlans = await _personPlanService.ListAsync();
        var resources = _mapper.Map<IEnumerable<PersonPlan>, IEnumerable<PersonPlanResource>>(personPlans);
        return resources;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPersonPlanById(int id)
    {
        var result = await _personPlanService.FindByIdAsync(id);

        var resource = _mapper.Map<PersonPlan, PersonPlanResource>(result);
        return Ok(resource);
    }
    
    [HttpGet("/api/v1/[controller]/person/{id}")]
    public async Task<IActionResult> GetPersonPlanByPersonId(int id)
    {
        var result = await _personPlanService.FindByPersonIdAsync(id);

        var resource = _mapper.Map<IEnumerable<PersonPlan>, IEnumerable<PersonPlanResource>>(result);
        return Ok(resource);
    }
    
    [HttpGet("/api/v1/[controller]/person/{id}/last")]
    public async Task<IActionResult> GetLastPersonPlanByPersonId(int id)
    {
        var result = await _personPlanService.FindLastByPersonIdAsync(id);

        var resource = _mapper.Map<PersonPlan, PersonPlanResource>(result);
        return Ok(resource);
    }
    
    [HttpGet("/api/v1/[controller]/plan/{id}")]
    public async Task<IActionResult> GetPersonPlanByPlanId(int id)
    {
        var result = await _personPlanService.FindByPlanAsync(id);

        var resource = _mapper.Map<IEnumerable<PersonPlan>, IEnumerable<PersonPlanResource>>(result);
        return Ok(resource);
    }
    
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SavePersonPlanResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var personPlan = _mapper.Map<SavePersonPlanResource, PersonPlan>(resource);

        var result = await _personPlanService.SaveAsync(personPlan);

        if (!result.Success)
            return BadRequest(result.Message);
        
        var personPlanResource = _mapper.Map<PersonPlan, PersonPlanResource>(result.Resource);

        return Ok(personPlanResource);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _personPlanService.Remove(id);
        
        if (!result.Success)
            return BadRequest(result.Message);
        
        var resource = _mapper.Map<PersonPlan, PersonPlanResource>(result.Resource);
        
        return Ok(resource);
    }
}