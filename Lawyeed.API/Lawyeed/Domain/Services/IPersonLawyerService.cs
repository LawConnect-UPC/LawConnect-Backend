using Lawyeed.API.Lawyeed.Domain.Models;
using Lawyeed.API.Lawyeed.Domain.Services.Communication;
using Lawyeed.API.Lawyeed.Domain.Services.Communication;

namespace Lawyeed.API.Lawyeed.Domain.Services;

public interface IPersonLawyerService
{
    Task<PersonLawyerResponse> LoginAsync(string email, string password);
    
    
    Task<IEnumerable<PersonLawyer>> ListAsync();

    Task<PersonLawyerResponse> SaveAsync(PersonLawyer personLawyer);

    Task<PersonLawyerResponse> FindByIdAsync(int id);

    Task<PersonLawyerResponse> UpdateAsync(int id, PersonLawyer personLawyer);

    Task<PersonLawyerResponse> DeleteAsync(int id);
}