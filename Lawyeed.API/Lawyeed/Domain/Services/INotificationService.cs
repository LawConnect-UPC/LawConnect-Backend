using Lawyeed.API.Lawyeed.Domain.Models;
using Lawyeed.API.Lawyeed.Domain.Services.Communication;

namespace Lawyeed.API.Lawyeed.Domain.Services;

public interface INotificationService 
{
    Task<IEnumerable<Notification>> ListAsync();
    Task<IEnumerable<Notification>> ListByPersonIdAsync(int personId);
    Task<NotificationResponse> FindByIdAsync(int id);
    Task<NotificationResponse> SaveAsync(Notification notification);
    Task<NotificationResponse> UpdateAsync(int id, Notification notification);
    Task<NotificationResponse> DeleteAsync(int id);
}