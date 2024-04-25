using Lawyeed.API.Lawyeed.Domain.Models;
using Lawyeed.API.Lawyeed.Domain.Repositories;
using Lawyeed.API.Lawyeed.Domain.Services;
using Lawyeed.API.Lawyeed.Domain.Services.Communication;

namespace Lawyeed.API.Lawyeed.Services;

public class PersonPlanService : IPersonPlanService
{
    private readonly IPersonPlanRepository _personPlanRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PersonPlanService(IPersonPlanRepository personPlanRepository, IUnitOfWork unitOfWork)
    {
        _personPlanRepository = personPlanRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<PersonPlan>> ListAsync()
    {
        return await _personPlanRepository.ListAsync();
    }

    public async Task<PersonPlanResponse> SaveAsync(PersonPlan personPlan)
    {
        try
        {
            await _personPlanRepository.AddAsync(personPlan);
            await _unitOfWork.CompleteAsync();

            return new PersonPlanResponse(personPlan);
        }
        catch (Exception e)
        {
            return new PersonPlanResponse("An error occurred while saving an Person Plan");
        }
    }

    public async Task<PersonPlan> FindByIdAsync(int id)
    {
        return await _personPlanRepository.FindByIdAsync(id);
    }

    
    public async Task<PersonPlanResponse> Remove(int  id)
    {
        var existingPersonPlan = await _personPlanRepository.FindByIdAsync(id);
        
        if (existingPersonPlan == null)
            return new PersonPlanResponse("Invalid Person Id");
        
        _personPlanRepository.Remove(existingPersonPlan);
        await _unitOfWork.CompleteAsync();

        return new PersonPlanResponse(existingPersonPlan);
    }

    public async Task<IEnumerable<PersonPlan>> FindByPersonIdAsync(int idPerson)
    {
        return await _personPlanRepository.FindByPersonIdAsync(idPerson);
    }

    public async Task<PersonPlan> FindLastByPersonIdAsync(int idPerson)
    {
        return await _personPlanRepository.FindLastByPersonIdAsync(idPerson);
    }

    public async Task<IEnumerable<PersonPlan>> FindByPlanAsync(int idPlan)
    {
        return await _personPlanRepository.FindByPlanAsync(idPlan);
    }
}