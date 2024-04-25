using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Lawyeed.API.Lawyeed.Domain.Models;
using Lawyeed.API.Lawyeed.Domain.Services;
using Lawyeed.API.Lawyeed.Resources;
using Lawyeed.API.Shared.Extensions;

namespace Lawyeed.API.Lawyeed.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class ConsultsController: ControllerBase
{
    private readonly IConsultService _consultService;
    private readonly IMapper _mapper;

    public ConsultsController(IConsultService consultService, IMapper mapper)
    {
        _consultService = consultService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<ConsultResource>> GetAllAsync()
    {
        var consults = await _consultService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Consult>, IEnumerable<ConsultResource>>(consults);
        return resources;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetConsultById(int id)
    {
        var result = await _consultService.FindByIdAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var resource = _mapper.Map<Consult, ConsultResource>(result.Resource);

        return Ok(resource);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveConsultResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var consult = _mapper.Map<SaveConsultResource, Consult>(resource);

        var result = await _consultService.SaveAsync(consult);

        if (!result.Success)
            return BadRequest(result.Message);

        var consultResource = _mapper.Map<Consult, ConsultResource>(result.Resource);

        return Ok(consultResource);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveConsultResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var consult = _mapper.Map<SaveConsultResource, Consult>(resource);

        var result = await _consultService.UpdateAsync(id, consult);

        if (!result.Success)
            return BadRequest(result.Message);

        var consultResource = _mapper.Map<Consult, ConsultResource>(result.Resource);

        return Ok(consultResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _consultService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var consultResource = _mapper.Map<Consult, ConsultResource>(result.Resource);

        return Ok(consultResource);
    }
    
    [HttpGet("/api/v1/persons/{id}/[controller]")]
    public async Task<IEnumerable<ConsultResource>> GetAllByClientIdAsync(int id)
    {
        var consults = await _consultService.ListByClientIdAsync(id);

        var resources = _mapper.Map<IEnumerable<Consult>, IEnumerable<ConsultResource>>(consults);

        return resources;
    }
    
    [HttpGet("/api/v1/personlawyers/{id}/[controller]")]
    public async Task<IEnumerable<ConsultResource>> GetAllByLawyerIdAsync(int id)
    {
        var consults = await _consultService.ListByLawyerIdAsync(id);

        var resources = _mapper.Map<IEnumerable<Consult>, IEnumerable<ConsultResource>>(consults);

        return resources;
    }
}