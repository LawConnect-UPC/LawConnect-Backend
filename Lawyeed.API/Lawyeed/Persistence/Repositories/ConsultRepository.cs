using Microsoft.EntityFrameworkCore;
using Lawyeed.API.Lawyeed.Domain.Models;
using Lawyeed.API.Lawyeed.Domain.Repositories;
using Lawyeed.API.Lawyeed.Persistence.Contexts;

namespace Lawyeed.API.Lawyeed.Persistence.Repositories;

public class ConsultRepository: BaseRepository,IConsultRepository
{
    public ConsultRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Consult>> ListAsync()
    {
        return await _context.Consults
            .Include(p => p.Client)
            .Include(p => p.Lawyer)
            .ToListAsync();
    }

    public async Task AddAsync(Consult consult)
    {
        await _context.Consults.AddAsync(consult);
    }

    public async Task<Consult> FindByIdAsync(int id)
    {
        return await _context.Consults
            .Include(p => p.Client)
            .Include(p => p.Lawyer)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Consult>> FindByLawyerIdAsync(int lawyerId)
    {
        return await _context.Consults
            .Where(p => p.LawyerId == lawyerId)
            .Include(p => p.Client)
            .Include(p => p.Lawyer)
            .ToListAsync();
    }

    public async Task<IEnumerable<Consult>> FindByClientIdAsync(int clientId)
    {
        return await _context.Consults
            .Where(p => p.ClientId == clientId)
            .Include(p => p.Client)
            .Include(p => p.Lawyer)
            .ToListAsync();
    }

    public void Update(Consult consult)
    {
        _context.Consults.Update(consult);
    }

    public void Remove(Consult consult)
    {
        _context.Consults.Remove(consult);
    }
}