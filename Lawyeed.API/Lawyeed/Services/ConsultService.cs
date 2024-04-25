using Lawyeed.API.Lawyeed.Domain.Models;
using Lawyeed.API.Lawyeed.Domain.Repositories;
using Lawyeed.API.Lawyeed.Domain.Services;
using Lawyeed.API.Lawyeed.Domain.Services.Communication;

namespace Lawyeed.API.Lawyeed.Services;

public class ConsultService: IConsultService
{
    
    private readonly IConsultRepository _consultRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPersonRepository _personRepository;
    
    public ConsultService(IConsultRepository consultRepository, IUnitOfWork unitOfWork, IPersonRepository personRepository)
    {
        _consultRepository = consultRepository;
        _unitOfWork = unitOfWork;
        _personRepository = personRepository;
        
    }
    
    public async Task<IEnumerable<Consult>> ListAsync()
    {
        return await _consultRepository.ListAsync();
    }
    
    public async Task<IEnumerable<Consult>> ListByLawyerIdAsync(int lawyerId)
    {
        return await _consultRepository.FindByLawyerIdAsync(lawyerId);
    }

    public async Task<IEnumerable<Consult>> ListByClientIdAsync(int clientId)
    {
        return await _consultRepository.FindByClientIdAsync(clientId);
    }
    
    public async Task<ConsultResponse> FindByIdAsync(int id)
    {
        var existingConsult = await _consultRepository.FindByIdAsync(id);

        if (existingConsult == null)
            return new ConsultResponse("Consult not found");

        return new ConsultResponse(existingConsult);
    }


    public async Task<ConsultResponse> SaveAsync(Consult consult)
    {
        try
        {
            await _consultRepository.AddAsync(consult);
            await _unitOfWork.CompleteAsync();

            return new ConsultResponse(consult);
        }
        catch (Exception e)
        {
            return new ConsultResponse($"An error occurred while saving the consult: {e.Message}");
        }
    }

    public async Task<ConsultResponse> UpdateAsync(int id, Consult consult)
    {
        var existingConsult = await _consultRepository.FindByIdAsync(id);

        if (existingConsult == null)
            return new ConsultResponse("Consult not found");
        
        var existingClient = await _personRepository.FindByIdAsync(consult.ClientId);

        if (existingClient == null)
            return new ConsultResponse("Invalid Client Account");

        var existingLawyer = await _personRepository.FindByIdAsync(consult.LawyerId);
        
        if (existingLawyer == null)
            return new ConsultResponse("Invalid Lawyer Account");

        existingConsult.Title = consult.Title;
        existingConsult.Description = consult.Description;
        existingConsult.State = consult.State;

        try
        {
            _consultRepository.Update(existingConsult);
            await _unitOfWork.CompleteAsync();

            return new ConsultResponse(existingConsult);
        }
        catch (Exception e)
        {
            return new ConsultResponse($"An error occurred while updating the consult: {e.Message}");
        }
    }

    public async Task<ConsultResponse> DeleteAsync(int id)
    {
        var existingConsult = await _consultRepository.FindByIdAsync(id);

        if (existingConsult == null)
            return new ConsultResponse("Consult not found");

        try
        {
            _consultRepository.Remove(existingConsult);
            await _unitOfWork.CompleteAsync();

            return new ConsultResponse(existingConsult);
        }
        catch (Exception e)
        {
            return new ConsultResponse($"An error occurred while deleting the consult: {e.Message}");
        }
    }
}