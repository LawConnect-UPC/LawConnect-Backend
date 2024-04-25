using Lawyeed.API.Lawyeed.Domain.Models;
using Lawyeed.API.Lawyeed.Domain.Repositories;
using Lawyeed.API.Lawyeed.Domain.Services;
using Lawyeed.API.Lawyeed.Domain.Services.Communication;

namespace Lawyeed.API.Lawyeed.Services;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public PersonService(IPersonRepository personRepository, IUnitOfWork unitOfWork)
    {
        _personRepository = personRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<PersonResponse> LoginAsync(string email, string password)
    {
        try
        {
            var person = await _personRepository.LoginAsync(email, password);
            if (person.Equals(null))
            {
                return new PersonResponse("Invalid Credentials");
            }
            return new PersonResponse(person);
        }
        catch (Exception e)
        {
            return new PersonResponse("Invalid Credentials"); 
        }
        
    }

    public async Task<IEnumerable<Person>> ListAsync()
    {
        return await _personRepository.ListAsync();
    }

    public async Task<PersonResponse> SaveAsync(Person person)
    {
        try
        {
            await _personRepository.AddAsync(person);
            await _unitOfWork.CompleteAsync();

            return new PersonResponse(person);
        }
        catch (Exception e)
        {
            return new PersonResponse("An error occurred while saving an Person");
        }
    }

    public async Task<Person> FindByIdAsync(int id)
    {
        return await _personRepository.FindByIdAsync(id);
    }

    public async Task<PersonResponse> UpdateAsync(int id, Person person)
    {
        var existingPerson = await _personRepository.FindByIdAsync(id);
        
        if (existingPerson == null)
            return new PersonResponse("Invalid Person Id");
        
        _personRepository.Update(person);
        await _unitOfWork.CompleteAsync();

        return new PersonResponse(person);
    }

    public async Task<PersonResponse> DeleteAsync(int id)
    {
        var existingPerson = await _personRepository.FindByIdAsync(id);
        
        if (existingPerson == null)
            return new PersonResponse("Invalid Person Id");
        
        _personRepository.Remove(existingPerson);
        await _unitOfWork.CompleteAsync();

        return new PersonResponse(existingPerson);
    }
}