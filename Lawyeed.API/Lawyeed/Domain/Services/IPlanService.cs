using Lawyeed.API.Lawyeed.Domain.Models;
using Lawyeed.API.Lawyeed.Domain.Services.Communication;

namespace Lawyeed.API.Lawyeed.Domain.Services;

public interface IPlanService
{
    Task<IEnumerable<Plan>> ListAsync();
    Task<PlanResponse> SaveAsync(Plan plan);
    Task<Plan> FindByIdAsync(int id);
    Task<PlanResponse> UpdateAsync(int id, Plan plan);
    Task<PlanResponse> DeleteAsync(int id);
}