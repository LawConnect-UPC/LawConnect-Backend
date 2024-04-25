using Microsoft.EntityFrameworkCore;
using Lawyeed.API.Lawyeed.Domain.Models;
using Lawyeed.API.Lawyeed.Domain.Repositories;
using Lawyeed.API.Lawyeed.Persistence.Contexts;

namespace Lawyeed.API.Lawyeed.Persistence.Repositories;

public class PersonLawyerRepository : BaseRepository, IPersonLawyerRepository
{
    public PersonLawyerRepository(AppDbContext context) : base(context)
    {
    }
    
    public async Task<PersonLawyer> LoginAsync(string email, string password)
    {
        return await _context.Lawyers.Where(p => p.Email == email &&  p.Password == password).FirstOrDefaultAsync();
    }
    public async Task<IEnumerable<PersonLawyer>> ListAsync()
    {
        return await _context.Lawyers.ToListAsync();
    }

    public async Task AddAsync(PersonLawyer personLawyer)
    {
        await _context.Lawyers.AddAsync(personLawyer);
    }

    public async Task<PersonLawyer> FindByIdAsync(int id)
    {
        return await _context.Lawyers.FirstOrDefaultAsync(p => p.Id == id);
    }

    public void Update(PersonLawyer personLawyer)
    {
        _context.Lawyers.Update(personLawyer);
    }

    public void Remove(PersonLawyer personLawyer)
    {
        _context.Lawyers.Remove(personLawyer);
    }

    public async Task<PersonLawyer> FindByEmailAsync(string email)
    {
        return await _context.Lawyers.SingleOrDefaultAsync(p => p.Email == email);
    }

    public bool ValidateEmail(string email)
    {
        return _context.Lawyers.Any(p => p.Email == email);
    }

    public PersonLawyer FindById(int id)
    {
        return _context.Lawyers.Find(id);
    }
}