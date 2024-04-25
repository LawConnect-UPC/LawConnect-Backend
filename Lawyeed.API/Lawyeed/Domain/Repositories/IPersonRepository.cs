using Lawyeed.API.Lawyeed.Domain.Models;

namespace Lawyeed.API.Lawyeed.Domain.Repositories;

public interface IPersonRepository
{
    Task<Person> LoginAsync(string email, string password);
    Task<IEnumerable<Person>> ListAsync();
    Task AddAsync(Person person);
    Task<Person> FindByIdAsync(int id);
    void Update(Person person);
    void Remove(Person person);
}