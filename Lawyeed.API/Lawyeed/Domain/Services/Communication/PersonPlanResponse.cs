using Lawyeed.API.Lawyeed.Domain.Models;
using Lawyeed.API.Shared.Domain.Services.Communication;

namespace Lawyeed.API.Lawyeed.Domain.Services.Communication;

public class PersonPlanResponse : BaseResponse<PersonPlan>
{
    public PersonPlanResponse(PersonPlan resource) : base(resource)
    {
    }

    public PersonPlanResponse(string message) : base(message)
    {
    }
}