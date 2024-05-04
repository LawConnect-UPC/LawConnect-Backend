using NUnit.Framework;
using Moq;
using System;
using System.Threading.Tasks;
using Lawyeed.API.Lawyeed.Domain.Models;
using Lawyeed.API.Lawyeed.Domain.Repositories;
using Lawyeed.API.Lawyeed.Domain.Services.Communication;
using Lawyeed.API.Lawyeed.Services;

namespace UnitTest;

[TestFixture]
public class MessageServiceTests
{
    private Mock<IMessageRepository> _mockMessageRepository;
    private Mock<IUnitOfWork> _mockUnitOfWork;
    private Mock<IConsultRepository> _mockConsultRepository;
    private Mock<IPersonRepository> _mockPersonRepository;
    private MessageService _messageService;

    [SetUp]
    public void Setup()
    {
        _mockMessageRepository = new Mock<IMessageRepository>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockConsultRepository = new Mock<IConsultRepository>();
        _mockPersonRepository = new Mock<IPersonRepository>();
        _messageService = new MessageService(_mockMessageRepository.Object, _mockUnitOfWork.Object, _mockConsultRepository.Object, _mockPersonRepository.Object);
    }

    [Test]
    public async Task SaveAsync_WithValidMessage_ShouldReturnSuccessResponse()
    {
        // Arrange
        var message = new Message { Id = 1, MessageToSend = "Hello", ConsultId = 1, PersonId = 1 };
        _mockConsultRepository.Setup(x => x.FindByIdAsync(message.ConsultId)).ReturnsAsync(new Consult());
        _mockPersonRepository.Setup(x => x.FindByIdAsync(message.PersonId)).ReturnsAsync(new Person());
        _mockMessageRepository.Setup(x => x.AddAsync(message)).Returns(Task.CompletedTask);
        _mockUnitOfWork.Setup(x => x.CompleteAsync()).Returns(Task.CompletedTask);

        // Act
        var result = await _messageService.SaveAsync(message);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.AreEqual(message, result.Resource);
    }

    [Test]
    public async Task SaveAsync_WithInvalidConsult_ShouldReturnErrorResponse()
    {
        // Arrange
        var message = new Message { Id = 1, MessageToSend = "Hello", ConsultId = 99, PersonId = 1 };
        _mockConsultRepository.Setup(x => x.FindByIdAsync(message.ConsultId)).ReturnsAsync((Consult)null);
        _mockPersonRepository.Setup(x => x.FindByIdAsync(message.PersonId)).ReturnsAsync(new Person());

        // Act
        var result = await _messageService.SaveAsync(message);

        // Assert
        Assert.IsFalse(result.Success);
        Assert.AreEqual("Invalid Consult", result.Message);
    }

    [Test]
    public async Task DeleteAsync_WithExistingMessage_ShouldReturnSuccessResponse()
    {
        // Arrange
        var message = new Message { Id = 1, MessageToSend = "Hello" };
        _mockMessageRepository.Setup(x => x.FindByIdAsync(message.Id)).ReturnsAsync(message);
        _mockMessageRepository.Setup(x => x.Remove(message));
        _mockUnitOfWork.Setup(x => x.CompleteAsync()).Returns(Task.CompletedTask);

        // Act
        var result = await _messageService.DeleteAsync(message.Id);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.AreEqual(message, result.Resource);
    }

    [Test]
    public async Task DeleteAsync_WithNonExistingMessage_ShouldReturnErrorResponse()
    {
        // Arrange
        _mockMessageRepository.Setup(x => x.FindByIdAsync(99)).ReturnsAsync((Message)null);

        // Act
        var result = await _messageService.DeleteAsync(99);

        // Assert
        Assert.IsFalse(result.Success);
        Assert.AreEqual("Message not found", result.Message);
    }
}
