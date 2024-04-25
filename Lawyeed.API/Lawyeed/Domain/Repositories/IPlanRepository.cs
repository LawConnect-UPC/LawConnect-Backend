using Lawyeed.API.Lawyeed.Domain.Models;

namespace Lawyeed.API.Lawyeed.Domain.Repositories;

public interface IPlanRepository
{
    Task<IEnumerable<Plan>> ListAsync(); 
    Task AddAsync(Plan plan);
    Task<Plan> FindByIdAsync(int id);
    void Update(Plan plan);
    void Remove(Plan plan);
}