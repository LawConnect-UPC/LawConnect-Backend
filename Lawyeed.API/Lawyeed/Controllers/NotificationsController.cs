using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Lawyeed.API.Lawyeed.Domain.Models;
using Lawyeed.API.Lawyeed.Domain.Services;
using Lawyeed.API.Lawyeed.Resources;
using Lawyeed.API.Shared.Extensions;

namespace Lawyeed.API.Lawyeed.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class NotificationsController: ControllerBase
{
    private readonly INotificationService _notificationService;
    private readonly IMapper _mapper;

    public NotificationsController(INotificationService notificationService, IMapper mapper)
    {
        _notificationService = notificationService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<NotificationResource>> GetAllAsync()
    {
        var notifications = await _notificationService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Notification>, IEnumerable<NotificationResource>>(notifications);
        return resources;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetConsultById(int id)
    {
        var result = await _notificationService.FindByIdAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var resource = _mapper.Map<Notification, NotificationResource>(result.Resource);

        return Ok(resource);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveNotificationResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var notification = _mapper.Map<SaveNotificationResource, Notification>(resource);

        var result = await _notificationService.SaveAsync(notification);

        if (!result.Success)
            return BadRequest(result.Message);

        var notificationResource = _mapper.Map<Notification, NotificationResource>(result.Resource);

        return Ok(notificationResource);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveNotificationResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var notification = _mapper.Map<SaveNotificationResource,Notification>(resource);

        var result = await _notificationService.UpdateAsync(id,notification);

        if (!result.Success)
            return BadRequest(result.Message);

        var notificationResource = _mapper.Map<Notification, NotificationResource>(result.Resource);

        return Ok(notificationResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _notificationService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var notificationResource = _mapper.Map<Notification, NotificationResource>(result.Resource);

        return Ok(notificationResource);
    }
    
    [HttpGet("persons/{id}")]
    public async Task<IEnumerable<NotificationResource>> GetAllByPersonIdAsync(int id)
    {
        var notifications = await _notificationService.ListByPersonIdAsync(id);
        var resources = _mapper.Map<IEnumerable<Notification>, IEnumerable<NotificationResource>>(notifications);
        return resources;
    }
}