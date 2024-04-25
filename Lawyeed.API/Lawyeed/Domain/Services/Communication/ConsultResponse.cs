using Lawyeed.API.Lawyeed.Domain.Models;
using Lawyeed.API.Shared.Domain.Services.Communication;

namespace Lawyeed.API.Lawyeed.Domain.Services.Communication;

public class ConsultResponse: BaseResponse<Consult>
{
    public ConsultResponse(Consult resource) : base(resource)
    {
    }
    
    public ConsultResponse(string message) : base(message)
    {
    }
}