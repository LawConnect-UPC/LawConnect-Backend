using Lawyeed.API.Lawyeed.Domain.Models;

namespace Lawyeed.API.Lawyeed.Domain.Repositories;

public interface IConsultRepository
{
    Task<IEnumerable<Consult>> ListAsync();
    
    Task AddAsync(Consult consult);
    
    Task<Consult> FindByIdAsync(int id);

    Task<IEnumerable<Consult>> FindByLawyerIdAsync(int lawyerId);
    
    Task<IEnumerable<Consult>> FindByClientIdAsync(int clientId);
    
    void Update(Consult consult);
    
    void Remove(Consult consult);
}