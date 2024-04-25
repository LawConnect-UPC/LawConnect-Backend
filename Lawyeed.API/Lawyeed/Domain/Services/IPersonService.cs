using Lawyeed.API.Lawyeed.Domain.Models;
using Lawyeed.API.Lawyeed.Domain.Services.Communication;

namespace Lawyeed.API.Lawyeed.Domain.Services;

public interface IPersonService
{
    Task<PersonResponse> LoginAsync(string email, string password);
    Task<IEnumerable<Person>> ListAsync();
    Task<PersonResponse> SaveAsync(Person person);
    Task<Person> FindByIdAsync(int id);
    Task<PersonResponse> UpdateAsync(int id, Person person);
    Task<PersonResponse> DeleteAsync(int id);
}