using Lawyeed.API.Lawyeed.Domain.Models;
using Lawyeed.API.Shared.Domain.Services.Communication;

namespace Lawyeed.API.Lawyeed.Domain.Services.Communication;

public class PersonLawyerResponse : BaseResponse<PersonLawyer>
{
    public PersonLawyerResponse(PersonLawyer resource) : base(resource)
    {
    }

    public PersonLawyerResponse(string message) : base(message)
    {
    }
}