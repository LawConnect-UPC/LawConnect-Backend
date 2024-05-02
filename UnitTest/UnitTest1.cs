using NUnit.Framework;
using System;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

using Lawyeed.API.Lawyeed.Domain.Models;
using Lawyeed.API.Lawyeed.Domain.Repositories;
using Lawyeed.API.Lawyeed.Domain.Services.Communication;
using Lawyeed.API.Lawyeed.Services;
namespace UnitTest;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }
    [Test]
    public async Task LoginAsync_WithInvalidCredentials_ShouldReturnPersonResponseWithError() // Test para verificar logeo de la aplicacion
    {
        // Arrange
        var mockPersonRepository = new Mock<IPersonRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        string email = "example@example.com";
        string password = "password";

        mockPersonRepository.Setup(repo => repo.LoginAsync(email, password)).ReturnsAsync((Person)null);

        var service = new PersonService(mockPersonRepository.Object, mockUnitOfWork.Object);

        // Act
        var result = await service.LoginAsync(email, password);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsFalse(result.Success);
        Assert.AreEqual("Invalid Credentials", result.Message);
    }
    [Test]
    public async Task ListAsync_ShouldReturnListOfPersons()   // Test para verificar la recupercaion de una lista de personas 
    {
        // Arrange
        var mockPersonRepository = new Mock<IPersonRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        mockPersonRepository.Setup(repo => repo.ListAsync()).ReturnsAsync(new List<Person>());

        var service = new PersonService(mockPersonRepository.Object, mockUnitOfWork.Object);

        // Act
        var result = await service.ListAsync();

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOf<IEnumerable<Person>>(result);
    }
    [Test]
    public async Task DeleteAsync_WithValidId_ShouldDeletePerson()  // Test para eliminar a una persona por ID
    {
        // Arrange
        var mockPersonRepository = new Mock<IPersonRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();

        int personId = 1;
        var existingPerson = new Person { Id = personId, FisrtName= "John", Email = "john@example.com", Password = "password" };

        mockPersonRepository.Setup(repo => repo.FindByIdAsync(personId)).ReturnsAsync(existingPerson);

        var service = new PersonService(mockPersonRepository.Object, mockUnitOfWork.Object);

        // Act
        var result = await service.DeleteAsync(personId);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsTrue(result.Success);
    }
//TEST DE CONSULTSERVICE
    // [Test]
    // public async Task ListAsync_ShouldReturnConsults() //Test que devuelve una lista válida de consultas  
    // {
    //     // Arrange
    //     var mockConsultRepository = new Mock<IConsultRepository>();
    //     var mockUnitOfWork = new Mock<IUnitOfWork>();
    //     var mockPersonRepository = new Mock<IPersonRepository>();
    //
    //     var consults = new List<Consult>
    //     {
    //         new Consult { Id = 1, Title = "Consult 1", Description = "Description 1" },
    //         new Consult { Id = 2, Title = "Consult 2", Description = "Description 2" }
    //     };
    //
    //     mockConsultRepository.Setup(repo => repo.ListAsync()).ReturnsAsync(consults);
    //     var service = new ConsultService(mockConsultRepository.Object, mockUnitOfWork.Object, mockPersonRepository.Object);
    //
    //     // Act
    //     var result = await service.ListAsync();
    //
    //     // Assert
    //     Assert.IsNotNull(result);
    //     Assert.IsInstanceOf<IEnumerable<Consult>>(result);
    //     Assert.AreEqual(2, ((List<Consult>)result).Count);
    // }
    // [Test]
    // public async Task ListByClientIdAsync_WithValidClientId_ShouldReturnConsults()//Test que devuelve lista de consultas de cliente por Id
    // {
    //     // Arrange
    //     int clientId = 456;
    //     var mockConsultRepository = new Mock<IConsultRepository>();
    //     mockConsultRepository.Setup(repo => repo.FindByClientIdAsync(clientId)).ReturnsAsync(new List<Consult>());
    //
    //     var service = new ConsultService(mockConsultRepository.Object, null, null);
    //
    //     // Act
    //     var result = await service.ListByClientIdAsync(clientId);
    //
    //     // Assert
    //     Assert.IsNotNull(result);
    //     Assert.IsInstanceOf<IEnumerable<Consult>>(result);
    // }
    // [Test]
    // public async Task ListByLawyerIdAsync_WithValidLawyerId_ShouldReturnConsults() //Test que devuelve lista de consultas de abogado por Id
    // {
    //     // Arrange
    //     int lawyerId = 123;
    //     var mockConsultRepository = new Mock<IConsultRepository>();
    //     mockConsultRepository.Setup(repo => repo.FindByLawyerIdAsync(lawyerId)).ReturnsAsync(new List<Consult>());
    //
    //     var service = new ConsultService(mockConsultRepository.Object, null, null);
    //
    //     // Act
    //     var result = await service.ListByLawyerIdAsync(lawyerId);
    //
    //     // Assert
    //     Assert.IsNotNull(result);
    //     Assert.IsInstanceOf<IEnumerable<Consult>>(result);
    // }
}

