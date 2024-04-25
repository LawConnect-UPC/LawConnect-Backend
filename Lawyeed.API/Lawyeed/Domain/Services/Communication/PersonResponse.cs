using Lawyeed.API.Lawyeed.Domain.Models;
using Lawyeed.API.Shared.Domain.Services.Communication;

namespace Lawyeed.API.Lawyeed.Domain.Services.Communication;

public class PersonResponse  : BaseResponse<Person>
{
    public PersonResponse(Person resource) : base(resource)
    {
    }

    public PersonResponse(string message) : base(message)
    {
    }
}