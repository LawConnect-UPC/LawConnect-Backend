using Lawyeed.API.Lawyeed.Domain.Models;
using Lawyeed.API.Lawyeed.Domain.Services.Communication;

namespace Lawyeed.API.Lawyeed.Domain.Services;

public interface IPersonPlanService
{
    
    Task<IEnumerable<PersonPlan>> ListAsync();
    Task<PersonPlanResponse> SaveAsync(PersonPlan personPlan);
    Task<PersonPlan> FindByIdAsync(int id);
    Task<PersonPlanResponse> Remove(int  id);
    
    Task<IEnumerable<PersonPlan>> FindByPersonIdAsync(int idPerson);
    Task<PersonPlan> FindLastByPersonIdAsync(int idPerson);
    Task<IEnumerable<PersonPlan>> FindByPlanAsync(int idPlan);
}