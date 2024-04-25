using Lawyeed.API.Lawyeed.Domain.Models;
using Lawyeed.API.Lawyeed.Domain.Services.Communication;

namespace Lawyeed.API.Lawyeed.Domain.Services;

public interface IConsultService
{
    Task<IEnumerable<Consult>> ListAsync();

    Task<ConsultResponse> FindByIdAsync(int id);
        
    Task<IEnumerable<Consult>> ListByLawyerIdAsync(int lawyerId);
    
    Task<IEnumerable<Consult>> ListByClientIdAsync(int clientId);
    
    Task<ConsultResponse> SaveAsync(Consult consult);
    
    Task<ConsultResponse> UpdateAsync(int id, Consult consult);
    
    Task<ConsultResponse> DeleteAsync(int id);
}