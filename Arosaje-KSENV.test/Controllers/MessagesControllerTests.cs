using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Arosaje_KSENV.Controllers;
using Arosaje_KSENV.Models;

namespace ArosajeKserv.Tests.Controllers
{
    [TestClass]
    public class MessagesControllerTests
    {
        private Mock<ArosajeKsenvContext> _mockContext;
        private MessagesController _controller;

        [TestInitialize]
        public void Initialize()
        {
            _mockContext = new Mock<ArosajeKsenvContext>();
            _controller = new MessagesController(_mockContext.Object);
        }

        [TestMethod]
        public async Task GetMessages_ReturnsOkResult_WithListOfMessages()
        {
            // Arrange
            var messages = new List<Message>
            {
                new Message { IdMessage = 1, Contenu = "Message 1" },
                new Message { IdMessage = 2, Contenu = "Message 2" }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Message>>();
            mockSet.As<IQueryable<Message>>().Setup(m => m.Provider).Returns(messages.Provider);
            mockSet.As<IQueryable<Message>>().Setup(m => m.Expression).Returns(messages.Expression);
            mockSet.As<IQueryable<Message>>().Setup(m => m.ElementType).Returns(messages.ElementType);
            mockSet.As<IQueryable<Message>>().Setup(m => m.GetEnumerator()).Returns(messages.GetEnumerator());

            _mockContext.Setup(c => c.Messages).Returns(mockSet.Object);

            // Act
            var result = await _controller.GetMessages();

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(messages);
        }

        [TestMethod]
        public async Task GetMessage_ReturnsNotFound_WhenMessageNotFound()
        {
            // Arrange
            var mockSet = new Mock<DbSet<Message>>();
            _mockContext.Setup(c => c.Messages).Returns(mockSet.Object);

            // Act
            var result = await _controller.GetMessage(1);

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public async Task GetMessage_ReturnsOkResult_WithMessage()
        {
            // Arrange
            var message = new Message { IdMessage = 1, Contenu = "Message 1" };
            var mockSet = new Mock<DbSet<Message>>();
            mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(message);

            _mockContext.Setup(c => c.Messages).Returns(mockSet.Object);

            // Act
            var result = await _controller.GetMessage(1);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(message);
        }

        [TestMethod]
        public async Task PostMessage_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var message = new Message { IdMessage = 1, Contenu = "Message 1" };
            var mockSet = new Mock<DbSet<Message>>();

            _mockContext.Setup(c => c.Messages).Returns(mockSet.Object);

            // Act
            var result = await _controller.PostMessage(message);

            // Assert
            result.Result.Should().BeOfType<CreatedAtActionResult>();
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            createdAtActionResult.StatusCode.Should().Be(201);
            createdAtActionResult.Value.Should().BeEquivalentTo(message);
        }

        [TestMethod]
        public async Task DeleteMessage_ReturnsNoContentResult()
        {
            // Arrange
            var message = new Message { IdMessage = 1, Contenu = "Message 1" };
            var mockSet = new Mock<DbSet<Message>>();
            mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(message);
            mockSet.Setup(m => m.Remove(message));

            _mockContext.Setup(c => c.Messages).Returns(mockSet.Object);

            // Act
            var result = await _controller.DeleteMessage(1);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [TestMethod]
        public async Task PutMessage_ReturnsNoContentResult()
        {
            // Arrange
            var message = new Message { IdMessage = 1, Contenu = "Message 1" };
            var mockSet = new Mock<DbSet<Message>>();
            mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(message);

            _mockContext.Setup(c => c.Messages).Returns(mockSet.Object);
            _mockContext.Setup(c => c.Entry(message).State == EntityState.Modified);

            // Act
            var result = await _controller.PutMessage(1, message);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }
    }
}
