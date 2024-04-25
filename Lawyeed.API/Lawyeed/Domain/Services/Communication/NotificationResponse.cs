using Lawyeed.API.Lawyeed.Domain.Models;
using Lawyeed.API.Shared.Domain.Services.Communication;

namespace Lawyeed.API.Lawyeed.Domain.Services.Communication;

public class NotificationResponse: BaseResponse<Notification>
{
    public NotificationResponse(Notification resource) : base(resource)
    {
    }
    
    public NotificationResponse(string message) : base(message)
    {
    }
}