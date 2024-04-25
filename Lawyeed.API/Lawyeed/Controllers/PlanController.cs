using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Lawyeed.API.Lawyeed.Domain.Models;
using Lawyeed.API.Lawyeed.Domain.Services;
using Lawyeed.API.Lawyeed.Resources;
using Lawyeed.API.Shared.Extensions;

namespace Lawyeed.API.Lawyeed.Controllers;

[Route("/api/v1/[controller]")]
public class PlanController : ControllerBase
{
    private readonly IPlanService _planService;
    private readonly IMapper _mapper;

    public PlanController(IPlanService planService, IMapper mapper)
    {
        _planService = planService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<PlanResource>> GetAllSync()
    {
        var plans = await _planService.ListAsync();
            
        var resources = _mapper.Map<IEnumerable<Plan>, IEnumerable<PlanResource>>(plans);
            
        return resources;
    }
    
    [HttpGet("{id}")]
    public async Task<PlanResource> GetPersonById(int id)
    {
        var plan = await _planService.FindByIdAsync(id);
        
        var resource = _mapper.Map<Plan, PlanResource>(plan);
        
        return resource;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SavePlanResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var plan = _mapper.Map<SavePlanResource, Plan>(resource);

        var result = await _planService.SaveAsync(plan);

        if (!result.Success)
            return BadRequest(result.Message);
        
        var planResource = _mapper.Map<Plan, PlanResource>(result.Resource);

        return Ok(planResource);
    }
    
    [HttpPut]
    public async Task<IActionResult> PutAsync([FromBody] SavePlanResource resource, int id)
    {
        var plan = _mapper.Map<SavePlanResource, Plan>(resource);
        
        var result = await _planService.UpdateAsync(id, plan);

        if (!result.Success)
            return BadRequest(result.Message);

        var planResource = _mapper.Map<Plan, PlanResource>(result.Resource);

        return Ok(planResource);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _planService.DeleteAsync(id);
        
        if (!result.Success)
            return BadRequest(result.Message);
        
        var resource = _mapper.Map<Plan, PlanResource>(result.Resource);
        
        return Ok(resource);
    }
}