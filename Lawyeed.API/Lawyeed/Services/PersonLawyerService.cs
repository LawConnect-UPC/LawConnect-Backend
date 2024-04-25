using Lawyeed.API.Lawyeed.Domain.Models;
using Lawyeed.API.Lawyeed.Domain.Repositories;
using Lawyeed.API.Lawyeed.Domain.Services;
using Lawyeed.API.Lawyeed.Domain.Services.Communication;

namespace Lawyeed.API.Lawyeed.Services;

public class PersonLawyerService : IPersonLawyerService
{
    private readonly IPersonLawyerRepository _personLawyerRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public PersonLawyerService(IPersonLawyerRepository personLawyerRepository, IUnitOfWork unitOfWork)
    {
        _personLawyerRepository = personLawyerRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<PersonLawyer>> ListAsync()
    {
        return await _personLawyerRepository.ListAsync();
    }
    public async Task<PersonLawyerResponse> LoginAsync(string email, string password)
    {
        try
        {
            var person = await _personLawyerRepository.LoginAsync(email, password);
            if (person.Equals(null))
            {
                return new PersonLawyerResponse("Invalid Credentials");
            }
            return new PersonLawyerResponse(person);
        }
        catch (Exception e)
        {
            return new PersonLawyerResponse("Invalid Credentials"); 
        }
        
    }
    public async Task<PersonLawyerResponse> SaveAsync(PersonLawyer personLawyer)
    {
        try
        {
            await _personLawyerRepository.AddAsync(personLawyer);
            await _unitOfWork.CompleteAsync();
            
            return new PersonLawyerResponse(personLawyer);
        }
        catch (Exception e)
        {
            return new PersonLawyerResponse("An error occurred while saving a Lawyer");
        }
    }

    public async Task<PersonLawyerResponse> FindByIdAsync(int id)
    {
        var existingLawyer = await _personLawyerRepository.FindByIdAsync(id);
        if (existingLawyer == null)
            return new PersonLawyerResponse("Invalid Lawyer Id");

        return new PersonLawyerResponse(existingLawyer);
    }

    public async Task<PersonLawyerResponse> UpdateAsync(int id, PersonLawyer personLawyer)
    {
        var existingLawyer = await _personLawyerRepository.FindByIdAsync(id);
        if (existingLawyer == null)
            return new PersonLawyerResponse("Invalid Lawyer Id");
        
        _personLawyerRepository.Update(personLawyer);
        await _unitOfWork.CompleteAsync();

        return new PersonLawyerResponse(personLawyer);
    }

    public async Task<PersonLawyerResponse> DeleteAsync(int id)
    {
        var existingLawyer = await _personLawyerRepository.FindByIdAsync(id);
        if (existingLawyer == null)
            return new PersonLawyerResponse("Invalid Lawyer Id");
        
        _personLawyerRepository.Remove(existingLawyer);
        await _unitOfWork.CompleteAsync();

        return new PersonLawyerResponse(existingLawyer);
    }
}