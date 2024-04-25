using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Lawyeed.API.Lawyeed.Domain.Models;
using Lawyeed.API.Lawyeed.Domain.Services;
using Lawyeed.API.Lawyeed.Resources;
using Lawyeed.API.Shared.Extensions;

namespace Lawyeed.API.Personal.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class MessagesController : ControllerBase
{
    private readonly IMessageService _messageService;
    private readonly IMapper _mapper;

    public MessagesController(IMessageService messageService, IMapper mapper)
    {
        _messageService = messageService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<MessageResource>> GetAllAsync()
    {
        var messages = await _messageService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Message>, IEnumerable<MessageResource>>(messages);
        return resources;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMessageById(int id)
    {
        var result = await _messageService.FindByIdAsync(id);

        if (!result.Success)
        {
            return BadRequest(result.Message);
        }

        var resource = _mapper.Map<Message, MessageResource>(result.Resource);

        return Ok(resource);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveMessageResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var message = _mapper.Map<SaveMessageResource, Message>(resource);

        var result = await _messageService.SaveAsync(message);

        if (!result.Success)
            return BadRequest(result.Message);

        var messageResource = _mapper.Map<Message, MessageResource>(result.Resource);

        return Ok(messageResource);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _messageService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var messageResource = _mapper.Map<Message, MessageResource>(result.Resource);

        return Ok(messageResource);
    }
    
    [HttpGet("{id}/consult")]
    public async Task<IEnumerable<MessageResource>> GetAllConsultIdAsync(int id)
    {
        var messages = await _messageService.ListByConsultId(id);
        var resources = _mapper.Map<IEnumerable<Message>, IEnumerable<MessageResource>>(messages);
        return resources;
    }
}