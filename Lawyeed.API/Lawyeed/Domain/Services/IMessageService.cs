using Lawyeed.API.Lawyeed.Domain.Models;
using Lawyeed.API.Lawyeed.Domain.Services.Communication;

namespace Lawyeed.API.Lawyeed.Domain.Services;

public interface IMessageService
{
    Task<IEnumerable<Message>> ListAsync();

    Task<MessageResponse> FindByIdAsync(int id);
    
    Task<IEnumerable<Message>> ListByConsultId(int consultId);
    
    Task<MessageResponse> SaveAsync(Message message);

    Task<MessageResponse> DeleteAsync(int id);
}