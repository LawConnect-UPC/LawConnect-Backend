using Microsoft.EntityFrameworkCore;
using Lawyeed.API.Lawyeed.Domain.Models;
using Lawyeed.API.Lawyeed.Domain.Repositories;
using Lawyeed.API.Lawyeed.Persistence.Contexts;

namespace Lawyeed.API.Lawyeed.Persistence.Repositories;

public class MessageRepository : BaseRepository, IMessageRepository
{
    public MessageRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Message>> ListAsync()
    {
        return await _context.Messages
            .Include(p => p.Consult)
            .Include(p => p.Person)
            .ToListAsync();
    }

    public async Task AddAsync(Message message)
    {
        await _context.Messages.AddAsync(message);
    }

    public async Task<Message> FindByIdAsync(int id)
    {
        return await _context.Messages
            .Include(p => p.Consult)
            .Include(p => p.Person)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Message>> FindByConsultId(int consultId)
    {
        return await _context.Messages
            .Where(p => p.ConsultId == consultId)
            .Include(p => p.Consult)
            .Include(p => p.Person)
            .ToListAsync();
    }

    public void Remove(Message message)
    {
        _context.Remove(message);
    }
}