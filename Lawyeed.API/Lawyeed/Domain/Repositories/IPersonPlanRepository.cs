using Lawyeed.API.Lawyeed.Domain.Models;

namespace Lawyeed.API.Lawyeed.Domain.Repositories;

public interface IPersonPlanRepository
{
    Task<IEnumerable<PersonPlan>> ListAsync();
    Task AddAsync(PersonPlan personPlan);
    Task<PersonPlan> FindByIdAsync(int id);
    void Update(PersonPlan personPlan);
    void Remove(PersonPlan personPlan);
    
    Task<IEnumerable<PersonPlan>> FindByPersonIdAsync(int idPerson);
    Task<PersonPlan> FindLastByPersonIdAsync(int idPerson);
    Task<IEnumerable<PersonPlan>> FindByPlanAsync(int idPlan);
}