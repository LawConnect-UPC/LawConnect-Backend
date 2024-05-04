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
using Lawyeed.API.Personal.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace TestIntegrations;

public class Tests
{
    
    private Mock<IMessageService> _mockMessageService;
    private Mock<IMapper> _mockMapper;
    private MessagesController _messagesController;
    
    [SetUp]
    public void Setup()
    {
        _mockMessageService = new Mock<IMessageService>();
        _mockMapper = new Mock<IMapper>();
        _messagesController = new MessagesController(_mockMessageService.Object, _mockMapper.Object);
    }
    
    [Test]
    public async Task GetAllAsync_ShouldReturnListOfNotificationResources()
    {
        // Arrange
        var mockNotificationService = new Mock<INotificationService>();
        var mockMapper = new Mock<IMapper>();
        mockNotificationService.Setup(service => service.ListAsync()).ReturnsAsync(new List<Notification>());
        mockMapper.Setup(mapper => mapper.Map<IEnumerable<Notification>, IEnumerable<NotificationResource>>(It.IsAny<IEnumerable<Notification>>()))
            .Returns(new List<NotificationResource>());

        var controller = new NotificationsController(mockNotificationService.Object, mockMapper.Object);

        // Act
        var result = await controller.GetAllAsync();

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOf<IEnumerable<NotificationResource>>(result);
    }
    [Test]
    public async Task GetAllAsync_ShouldReturnListOfConsultResources()
    {
        // Arrange
        var mockConsultService = new Mock<IConsultService>();
        var mockMapper = new Mock<IMapper>();
        mockConsultService.Setup(service => service.ListAsync()).ReturnsAsync(new List<Consult>());
        mockMapper.Setup(mapper => mapper.Map<IEnumerable<Consult>, IEnumerable<ConsultResource>>(It.IsAny<IEnumerable<Consult>>()))
            .Returns(new List<ConsultResource>());
    
        var controller = new ConsultsController(mockConsultService.Object, mockMapper.Object);
    
        // Act
        var result = await controller.GetAllAsync();
    
        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOf<IEnumerable<ConsultResource>>(result);
    }
    [Test]
    public async Task GetAllSync_ShouldReturnListOfPlanResources()  //Test para devolver una lista de planes
    {
        // Arrange
        var mockPlanService = new Mock<IPlanService>();
        var mockMapper = new Mock<IMapper>();
        mockPlanService.Setup(service => service.ListAsync()).ReturnsAsync(new List<Plan>());
        mockMapper.Setup(mapper => mapper.Map<IEnumerable<Plan>, IEnumerable<PlanResource>>(It.IsAny<IEnumerable<Plan>>()))
            .Returns(new List<PlanResource>());
    
        var controller = new PlanController(mockPlanService.Object, mockMapper.Object);
    
        // Act
        var result = await controller.GetAllSync();
    
        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOf<IEnumerable<PlanResource>>(result);
    }
    
    // MESSAGE CONTROLLER TESTS
    [Test]
    public async Task GetAllAsync_ShouldReturnListOfMessageResources()   //Test para devolver una lista de mensajes
    {
        // Arrange
        var mockMessageService = new Mock<IMessageService>();
        var mockMapper = new Mock<IMapper>();
        mockMessageService.Setup(service => service.ListAsync()).ReturnsAsync(new List<Message>());
        mockMapper.Setup(mapper => mapper.Map<IEnumerable<Message>, IEnumerable<MessageResource>>(It.IsAny<IEnumerable<Message>>()))
            .Returns(new List<MessageResource>());
    
        var controller = new MessagesController(mockMessageService.Object, mockMapper.Object);
    
        // Act
        var result = await controller.GetAllAsync();
    
        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOf<IEnumerable<MessageResource>>(result);
    }

        [Test]
    public async Task GetMessageById_ValidId_ShouldReturnMessage()
    {
        // Arrange
        var message = new Message { Id = 1, MessageToSend = "Hello" };
        var messageResource = new MessageResource { Id = 1, MessageToSend = "Hello" };

        _mockMessageService.Setup(s => s.FindByIdAsync(1)).ReturnsAsync(new MessageResponse(message));
        _mockMapper.Setup(m => m.Map<Message, MessageResource>(It.IsAny<Message>())).Returns(messageResource);

        // Act
        var result = await _messagesController.GetMessageById(1);

        // Assert
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
        Assert.AreEqual(messageResource, okResult.Value);
    }

    [Test]
    public async Task GetMessageById_InvalidId_ShouldReturnBadRequest()
    {
        // Arrange
        _mockMessageService.Setup(s => s.FindByIdAsync(99)).ReturnsAsync(new MessageResponse("Message not Found"));

        // Act
        var result = await _messagesController.GetMessageById(99);

        // Assert
        var badRequestResult = result as BadRequestObjectResult;
        Assert.IsNotNull(badRequestResult);
        Assert.AreEqual(400, badRequestResult.StatusCode);
        Assert.AreEqual("Message not Found", badRequestResult.Value);
    }

    [Test]
    public async Task PostAsync_ValidRequest_ShouldCreateMessage()
    {
        // Arrange
        var saveMessageResource = new SaveMessageResource { MessageToSend = "Hello" };
        var message = new Message { MessageToSend = "Hello" };
        var messageResource = new MessageResource { MessageToSend = "Hello" };

        _mockMapper.Setup(m => m.Map<SaveMessageResource, Message>(It.IsAny<SaveMessageResource>())).Returns(message);
        _mockMessageService.Setup(s => s.SaveAsync(It.IsAny<Message>())).ReturnsAsync(new MessageResponse(message));
        _mockMapper.Setup(m => m.Map<Message, MessageResource>(It.IsAny<Message>())).Returns(messageResource);

        // Act
        var result = await _messagesController.PostAsync(saveMessageResource);

        // Assert
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
        Assert.AreEqual(messageResource, okResult.Value);
    }

    // PERSON CONTROLLER TESTS
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