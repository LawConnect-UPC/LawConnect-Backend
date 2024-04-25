using Microsoft.EntityFrameworkCore;
using Lawyeed.API.Lawyeed.Domain.Models;
using Lawyeed.API.Lawyeed.Domain.Repositories;
using Lawyeed.API.Lawyeed.Persistence.Contexts;

namespace Lawyeed.API.Lawyeed.Persistence.Repositories;

public class PersonRepository : BaseRepository, IPersonRepository
{
    public PersonRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Person> LoginAsync(string email, string password)
    {
        return await _context.Persons.Where(p => p.Email == email &&  p.Password == password).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Person>> ListAsync()
    {
        return await _context.Persons.Where(p => p.Type == "client").ToListAsync();
    }

    public async Task AddAsync(Person person)
    {
        await _context.Persons.AddAsync(person);
    }

    public async Task<Person> FindByIdAsync(int id)
    {
        return await _context.Persons.FindAsync(id);
    }

    public void Update(Person person)
    {
        _context.Update(person);
    }

    public void Remove(Person person)
    {
        _context.Remove(person);
    }
}