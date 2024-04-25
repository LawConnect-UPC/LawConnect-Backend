using Lawyeed.API.Lawyeed.Domain.Models;
using Lawyeed.API.Shared.Domain.Services.Communication;

namespace Lawyeed.API.Lawyeed.Domain.Services.Communication;

public class MessageResponse : BaseResponse<Message>
{
    public MessageResponse(Message resource) : base(resource)
    {
    }

    public MessageResponse(string message) : base(message)
    {
    }
}