using System.Numerics;
using Lawyeed.API.Lawyeed.Domain.Models;
using Lawyeed.API.Lawyeed.Domain.Repositories;
using Lawyeed.API.Lawyeed.Domain.Services;
using Lawyeed.API.Lawyeed.Domain.Services.Communication;

namespace Lawyeed.API.Lawyeed.Services;

public class PlanService : IPlanService
{
    private readonly IPlanRepository _planRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PlanService(IPlanRepository planRepository, IUnitOfWork unitOfWork)
    {
        _planRepository = planRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Plan>> ListAsync()
    {
        return await _planRepository.ListAsync();
    }

    public async Task<PlanResponse> SaveAsync(Plan plan)
    {
        try
        {
            await _planRepository.AddAsync(plan);
            await _unitOfWork.CompleteAsync();

            return new PlanResponse(plan);
        }
        catch (Exception e)
        {
            return new PlanResponse("An error occurred while saving an Plan");
        }
    }

    public async Task<Plan> FindByIdAsync(int id)
    {
        return await _planRepository.FindByIdAsync(id);
    }

    public async Task<PlanResponse> UpdateAsync(int id, Plan plan)
    {
        var existingPlan = await _planRepository.FindByIdAsync(id);
        
        if (existingPlan == null)
            return new PlanResponse("Invalid Plan Id");

        existingPlan.Name = plan.Name;
        existingPlan.Description = plan.Description;
        existingPlan.Price = plan.Price;
        
        _planRepository.Update(plan);
        await _unitOfWork.CompleteAsync();

        return new PlanResponse(existingPlan);
    }

    public async Task<PlanResponse> DeleteAsync(int id)
    {
        var existingPlan = await _planRepository.FindByIdAsync(id);
        
        if (existingPlan == null)
            return new PlanResponse("Invalid Plan Id");
        
        _planRepository.Remove(existingPlan);
        await _unitOfWork.CompleteAsync();

        return new PlanResponse(existingPlan);
    }
}