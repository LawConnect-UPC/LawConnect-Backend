using Lawyeed.API.Lawyeed.Domain.Models;
using Lawyeed.API.Lawyeed.Domain.Repositories;
using Lawyeed.API.Lawyeed.Domain.Services;
using Lawyeed.API.Lawyeed.Domain.Services.Communication;

namespace Lawyeed.API.Lawyeed.Services;

public class NotificationService: INotificationService
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConsultRepository _consultRepository;
    private readonly IPersonRepository _personRepository;

    public NotificationService(INotificationRepository notificationRepository, IUnitOfWork unitOfWork, IConsultRepository consultRepository, IPersonRepository personRepository)
    {
        _notificationRepository = notificationRepository;
        _unitOfWork = unitOfWork;
        _consultRepository = consultRepository;
        _personRepository = personRepository;
    }
    
    public async Task<IEnumerable<Notification>> ListAsync()
    {
        return await _notificationRepository.ListAsync();
    }

    public async Task<IEnumerable<Notification>> ListByPersonIdAsync(int personId)
    {
        return await _notificationRepository.FindByPersonIdAsync(personId);
    }
    
    public async Task<NotificationResponse> FindByIdAsync(int id)
    {
        var existingNotification = await _notificationRepository.FindByIdAsync(id);

        if (existingNotification == null)
            return new NotificationResponse("Notification not found");

        return new NotificationResponse(existingNotification);
    }

    public async Task<NotificationResponse> SaveAsync(Notification notification)
    {
        var existingPerson = await _personRepository.FindByIdAsync(notification.PersonId);

        if (existingPerson == null)
            return new NotificationResponse("Invalid Person Account");

        var existingConsult = await _consultRepository.FindByIdAsync(notification.ConsultId);
        
        if (existingConsult == null)
            return new NotificationResponse("Invalid Consult");

        try
        {
            await _notificationRepository.AddAsync(notification);
            await _unitOfWork.CompleteAsync();

            return new NotificationResponse(notification);
        }
        catch (Exception e)
        {
            return new NotificationResponse($"An error occurred while saving the notification: {e.Message}");
        }
    }

    public async Task<NotificationResponse> UpdateAsync(int id, Notification notification)
    {
        var existingPerson = await _personRepository.FindByIdAsync(notification.PersonId);

        if (existingPerson == null)
            return new NotificationResponse("Invalid Person Account");

        var existingConsult = await _consultRepository.FindByIdAsync(notification.ConsultId);
        
        if (existingConsult == null)
            return new NotificationResponse("Invalid Consult");

        var existingNotification = await _notificationRepository.FindByIdAsync(id);

        if (existingNotification == null)
            return new NotificationResponse("Notification not found");

        existingNotification.Title = notification.Title;
        existingNotification.Description = notification.Description;

        try
        {
            _notificationRepository.Update(existingNotification);
            await _unitOfWork.CompleteAsync();

            return new NotificationResponse(existingNotification);
        }
        catch (Exception e)
        {
            return new NotificationResponse($"An error occurred while updating the notification: {e.Message}");
        }
    }

    public async Task<NotificationResponse> DeleteAsync(int id)
    {
        var existingNotification = await _notificationRepository.FindByIdAsync(id);

        if (existingNotification == null)
            return new NotificationResponse("Notification not found");
        
        try
        {
            _notificationRepository.Remove(existingNotification);
            await _unitOfWork.CompleteAsync();

            return new NotificationResponse(existingNotification);
        }
        catch (Exception e)
        {
            return new NotificationResponse($"An error occurred while deleting the notification: {e.Message}");
        }
    }
}