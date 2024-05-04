namespace UnitTest;
using NUnit.Framework;
using Moq;
using System;
using System.Threading.Tasks;
using Lawyeed.API.Lawyeed.Domain.Models;
using Lawyeed.API.Lawyeed.Domain.Repositories;
using Lawyeed.API.Lawyeed.Domain.Services.Communication;
using Lawyeed.API.Lawyeed.Services;


[TestFixture]
public class ConsultServiceTests
{
    private Mock<IConsultRepository> _mockConsultRepository;
    private Mock<IUnitOfWork> _mockUnitOfWork;
    private Mock<IPersonRepository> _mockPersonRepository;
    private ConsultService _consultService;

    [SetUp]
    public void Setup()
    {
        _mockConsultRepository = new Mock<IConsultRepository>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockPersonRepository = new Mock<IPersonRepository>();
        _consultService = new ConsultService(_mockConsultRepository.Object, _mockUnitOfWork.Object, _mockPersonRepository.Object);
    }

    [Test]
    public async Task SaveAsync_WithValidData_ShouldReturnSuccessResponse()
    {
        // Arrange
        var consult = new Consult { Title = "New Consult", Description = "Description of consult", ClientId = 1, LawyerId = 1 };
        _mockConsultRepository.Setup(x => x.AddAsync(consult)).Returns(Task.CompletedTask);
        _mockUnitOfWork.Setup(x => x.CompleteAsync()).Returns(Task.CompletedTask);

        // Act
        var result = await _consultService.SaveAsync(consult);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.AreEqual(consult, result.Resource);
        _mockConsultRepository.Verify(x => x.AddAsync(consult), Times.Once);
        _mockUnitOfWork.Verify(x => x.CompleteAsync(), Times.Once);
    }

    [Test]
    public async Task SaveAsync_WithException_ShouldReturnErrorResponse()
    {
        // Arrange
        var consult = new Consult { Title = "New Consult", Description = "Test", ClientId = 1, LawyerId = 1 };
        _mockConsultRepository.Setup(x => x.AddAsync(consult)).ThrowsAsync(new Exception("Database error"));

        // Act
        var result = await _consultService.SaveAsync(consult);

        // Assert
        Assert.IsFalse(result.Success);
        Assert.IsNull(result.Resource);
        Assert.AreEqual("An error occurred while saving the consult: Database error", result.Message);
        _mockConsultRepository.Verify(x => x.AddAsync(consult), Times.Once);
        _mockUnitOfWork.Verify(x => x.CompleteAsync(), Times.Never);
    }
}