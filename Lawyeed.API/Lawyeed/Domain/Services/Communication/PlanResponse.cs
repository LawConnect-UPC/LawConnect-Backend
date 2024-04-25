using Lawyeed.API.Lawyeed.Domain.Models;
using Lawyeed.API.Shared.Domain.Services.Communication;

namespace Lawyeed.API.Lawyeed.Domain.Services.Communication;

public class PlanResponse : BaseResponse<Plan>
{
    public PlanResponse(Plan resource) : base(resource)
    {
    }

    public PlanResponse(string message) : base(message)
    {
    }
}