using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Lawyeed.API.Lawyeed.Domain.Models;
using Lawyeed.API.Lawyeed.Domain.Services;
using Lawyeed.API.Lawyeed.Resources;
using Lawyeed.API.Shared.Extensions;

namespace Lawyeed.API.Lawyeed.Controllers;

[Produces("application/json")]
[ApiController]
[Route("/api/v1/[controller]")]
public class PersonController: ControllerBase
{
    private readonly IPersonService _personService;
    private readonly IMapper _mapper;

    public PersonController(IPersonService personService, IMapper mapper)
    {
        _personService = personService;
        _mapper = mapper;
    }
    
    
    
    [HttpGet]
    public async Task<IEnumerable<PersonResource>> GetAllSync()
    {
        var persons = await _personService.ListAsync();
            
        var resources = _mapper.Map<IEnumerable<Person>, IEnumerable<PersonResource>>(persons);
            
        return resources;
    }
    [HttpGet("{id}")]
    public async Task<PersonResource> GetPersonById(int id)
    {
        var person = await _personService.FindByIdAsync(id);
        
        var resource = _mapper.Map<Person, PersonResource>(person);
        
        return resource;
    }

    [HttpPost("register")]
    public async Task<IActionResult> PostAsync([FromBody] SavePersonResource resource)
    {
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var person = _mapper.Map<SavePersonResource, Person>(resource);

        var result = await _personService.SaveAsync(person);

        if (!result.Success)
            return BadRequest(result.Message);
        
        var personResource = _mapper.Map<Person, PersonResource>(result.Resource);

        return Ok(personResource);
    }
    [HttpGet("login")]
    public async Task<IActionResult> LoginAsync([FromQuery] string email, string password)
    {

        var result = await _personService.LoginAsync(email, password);
        
        if (!result.Success)
            return BadRequest(result.Message);
        
        var resource = _mapper.Map<Person, PersonResource>(result.Resource);

        return Ok(resource);
    }
    [HttpPut]
    public async Task<IActionResult> PutAsync([FromBody] SavePersonResource resource, int id)
    {
        var person = _mapper.Map<SavePersonResource, Person>(resource);
        
        var result = await _personService.UpdateAsync(id, person);

        if (!result.Success)
            return BadRequest(result.Message);

        var personResource = _mapper.Map<Person, PersonResource>(result.Resource);

        return Ok(personResource);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _personService.DeleteAsync(id);
        
        if (!result.Success)
            return BadRequest(result.Message);
        
        var resource = _mapper.Map<Person, PersonResource>(result.Resource);
        
        return Ok(resource);
    }
}