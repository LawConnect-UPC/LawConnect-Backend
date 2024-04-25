using Lawyeed.API.Lawyeed.Domain.Models;
using Lawyeed.API.Lawyeed.Domain.Repositories;
using Lawyeed.API.Lawyeed.Domain.Services;
using Lawyeed.API.Lawyeed.Domain.Services.Communication;

namespace Lawyeed.API.Lawyeed.Services;

public class MessageService : IMessageService
{
    private readonly IMessageRepository _messageRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConsultRepository _consultRepository;
    private readonly IPersonRepository _personRepository;

    public MessageService(IMessageRepository messageRepository, IUnitOfWork unitOfWork, IConsultRepository consultRepository, IPersonRepository personRepository)
    {
        _messageRepository = messageRepository;
        _unitOfWork = unitOfWork;
        _consultRepository = consultRepository;
        _personRepository = personRepository;
    }

    public async Task<IEnumerable<Message>> ListAsync()
    {
        return await _messageRepository.ListAsync();
    }

    public async Task<MessageResponse> FindByIdAsync(int id)
    {
        var existingMessage = await _messageRepository.FindByIdAsync(id);

        if (existingMessage == null)
        {
            return new MessageResponse("Message not Found");    
        }

        return new MessageResponse(existingMessage);
    }

    public async Task<IEnumerable<Message>> ListByConsultId(int consultId)
    {
        return await _messageRepository.FindByConsultId(consultId);
    }

    public async Task<MessageResponse> SaveAsync(Message message)
    {
        var existingConsult = await _consultRepository.FindByIdAsync(message.ConsultId);

        if (existingConsult == null)
        {
            return new MessageResponse("Invalid Consult");
        }

        var existingPerson = await _personRepository.FindByIdAsync(message.PersonId);

        if (existingPerson == null)
        {
            return new MessageResponse("Invalid Person Account");
        }

        try
        {
            await _messageRepository.AddAsync(message);
            await _unitOfWork.CompleteAsync();

            return new MessageResponse(message);
        }
        catch (Exception e)
        {
            return new MessageResponse($"An error occurred while saving the notification: {e.Message}");
        }
    }

    public async Task<MessageResponse> DeleteAsync(int id)
    {
        var existingMessage = await _messageRepository.FindByIdAsync(id);

        if (existingMessage == null)
        {
            return new MessageResponse("Message not found");
        }

        try
        {
            _messageRepository.Remove(existingMessage);
            await _unitOfWork.CompleteAsync();

            return new MessageResponse(existingMessage);
        }
        catch (Exception e)
        {
            return new MessageResponse($"An error occurred while deleting the notification: {e.Message}");
        }
    }
}