using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Lawyeed.API.Lawyeed.Domain.Models;
using Lawyeed.API.Lawyeed.Domain.Repositories;
using Lawyeed.API.Lawyeed.Persistence.Contexts;

namespace Lawyeed.API.Lawyeed.Persistence.Repositories;

public class NotificationRepository: BaseRepository,INotificationRepository
{
    public NotificationRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Notification>> ListAsync()
    {
        return await _context.Notifications
            .Include(p => p.Consult)
            .Include(p => p.Person)
            .ToListAsync();
    }

    public async Task AddAsync(Notification consult)
    {
        await _context.Notifications.AddAsync(consult);
    }

    public async Task<Notification> FindByIdAsync(int id)
    {
        return await _context.Notifications
            .Include(p => p.Consult)
            .Include(p => p.Person)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Notification>> FindByPersonIdAsync(int personId)
    {
        return await _context.Notifications
            .Where(p => p.PersonId == personId)
            .Include(p => p.Consult)
            .Include(p => p.Person)
            .ToListAsync();
    }

    public void Update(Notification consult)
    {
        _context.Notifications.Update(consult);
    }

    public void Remove(Notification consult)
    {
        _context.Notifications.Remove(consult);
    }
}