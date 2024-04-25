using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Lawyeed.API.Lawyeed.Domain.Models;
using Lawyeed.API.Lawyeed.Domain.Services;
using Lawyeed.API.Lawyeed.Resources;
using Lawyeed.API.Shared.Extensions;

namespace Lawyeed.API.Lawyeed.Controllers;

[Route("/api/v1/[controller]")]
[ApiController]
public class PersonLawyersController : ControllerBase
{
    private readonly IPersonLawyerService _personLawyerService;
    private readonly IMapper _mapper;

    public PersonLawyersController(IPersonLawyerService personLawyerService, IMapper mapper)
    {
        _personLawyerService = personLawyerService;
        _mapper = mapper;
    }
    
    [HttpGet("login")]
    public async Task<IActionResult> LoginAsync([FromQuery] string email, string password)
    {

        var result = await _personLawyerService.LoginAsync(email, password);
        
        if (!result.Success)
            return BadRequest(result.Message);
        
        var resource = _mapper.Map<PersonLawyer, PersonLawyerResource>(result.Resource);

        return Ok(resource);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetLawyerById(int id)
    {
        var result = await _personLawyerService.FindByIdAsync(id);
        
        if (!result.Success)
            return BadRequest(result.Message);
        
        var resource = _mapper.Map<PersonLawyer, PersonLawyerResource>(result.Resource);
        return Ok(resource);
    }

    [HttpGet]
    public async Task<IEnumerable<PersonLawyerResource>> GetAllAsync()
    {
        var lawyers = await _personLawyerService.ListAsync();
        var resources = _mapper.Map<IEnumerable<PersonLawyer>, IEnumerable<PersonLawyerResource>>(lawyers);
        return resources;
    }

    [HttpPost("register")]
    public async Task<IActionResult> PostAsync([FromBody] SavePersonLawyerResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        //Mapear el objeto a una entidad
        var lawyer = _mapper.Map<SavePersonLawyerResource, PersonLawyer>(resource);
        var result = await _personLawyerService.SaveAsync(lawyer);

        if (!result.Success)
            return BadRequest(result.Message);

        var lawyerResource = _mapper.Map<PersonLawyer, PersonLawyerResource>(result.Resource);
        return Ok(lawyerResource);
    }

    [HttpPut]
    public async Task<IActionResult> PutAsync([FromBody] SavePersonLawyerResource resource, int id)
    {
        var lawyer = _mapper.Map<SavePersonLawyerResource, PersonLawyer>(resource);
        var result = await _personLawyerService.UpdateAsync(id, lawyer);

        if (!result.Success)
            return BadRequest(result.Message);

        var lawyerResource = _mapper.Map<PersonLawyer, PersonLawyerResource>(result.Resource);

        return Ok(lawyerResource);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _personLawyerService.DeleteAsync(id);
        
        if (!result.Success)
            return BadRequest(result.Message);
        var lawyerResource = _mapper.Map<PersonLawyer, PersonLawyerResource>(result.Resource);
        
        return Ok(lawyerResource);
    }
}