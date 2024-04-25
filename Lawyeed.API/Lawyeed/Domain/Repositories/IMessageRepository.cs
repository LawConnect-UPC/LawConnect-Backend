using Lawyeed.API.Lawyeed.Domain.Models;

namespace Lawyeed.API.Lawyeed.Domain.Repositories;

public interface IMessageRepository
{
    Task<IEnumerable<Message>> ListAsync();

    Task AddAsync(Message message);

    Task<Message> FindByIdAsync(int id);

    Task<IEnumerable<Message>> FindByConsultId(int consultId);

    void Remove(Message message);
}