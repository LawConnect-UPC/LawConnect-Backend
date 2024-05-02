using NUnit.Framework;
using System;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Lawyeed.API.Lawyeed.Controllers;
using Lawyeed.API.Lawyeed.Domain.Models;
using Lawyeed.API.Lawyeed.Domain.Repositories;
using Lawyeed.API.Lawyeed.Domain.Services;
using Lawyeed.API.Lawyeed.Domain.Services.Communication;
using Lawyeed.API.Lawyeed.Resources;
using Lawyeed.API.Lawyeed.Services;
namespace TestIntegrations;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task GetAllSync_ShouldReturnListOfPersonResources()  //Test para devolver una lista de personas 
    {
        // Arrange
        var mockPersonService = new Mock<IPersonService>();
        var mockMapper = new Mock<IMapper>();
        mockPersonService.Setup(service => service.ListAsync()).ReturnsAsync(new List<Person>());
        mockMapper.Setup(mapper => mapper.Map<IEnumerable<Person>, IEnumerable<PersonResource>>(It.IsAny<IEnumerable<Person>>()))
            .Returns(new List<PersonResource>());

        var controller = new PersonController(mockPersonService.Object, mockMapper.Object);

        // Act
        var result = await controller.GetAllSync();

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOf<IEnumerable<PersonResource>>(result);
    }
}