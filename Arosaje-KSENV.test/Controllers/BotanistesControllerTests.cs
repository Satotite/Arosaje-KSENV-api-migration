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
    public class BotanistesControllerTests
    {
        private Mock<ArosajeKsenvContext> _mockContext;
        private BotanistesController _controller;

        [TestInitialize]
        public void Initialize()
        {
            _mockContext = new Mock<ArosajeKsenvContext>();
            _controller = new BotanistesController(_mockContext.Object);
        }

        [TestMethod]
        public async Task GetBotanistes_ReturnsOkResult_WithListOfBotanistes()
        {
            // Arrange
            var botanistes = new List<Botaniste>
            {
                new Botaniste { IdUtilisateur = 1 },
                new Botaniste { IdUtilisateur = 2 }
            }.AsQueryable();

            var mockSet = MockDbSetExtensions.BuildMockDbSet(botanistes);

            _mockContext.Setup(c => c.Botanistes).Returns(mockSet.Object);

            // Act
            var result = await _controller.GetBotanistes();

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            var returnBotanistes = okResult.Value as IEnumerable<Botaniste>;
            returnBotanistes.Should().NotBeNull();
            returnBotanistes.Count().Should().Be(2);
        }

        [TestMethod]
        public async Task GetBotaniste_ReturnsNotFound_WhenBotanisteNotFound()
        {
            // Arrange
            var mockSet = new Mock<DbSet<Botaniste>>();
            _mockContext.Setup(c => c.Botanistes).Returns(mockSet.Object);

            // Act
            var result = await _controller.GetBotaniste(1);

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public async Task GetBotaniste_ReturnsOkResult_WithBotaniste()
        {
            var botaniste = new Botaniste { IdUtilisateur = 1 };
            var mockSet = new Mock<DbSet<Botaniste>>();

            mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(botaniste);

            _mockContext.Setup(c => c.Botanistes).Returns(mockSet.Object);

            // Act
            var result = await _controller.GetBotaniste(1);

            // Assert
            result.Should().NotBeNull();

            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);

            var returnBotaniste = okResult.Value as Botaniste;
            returnBotaniste.Should().NotBeNull();
            returnBotaniste.Should().BeEquivalentTo(botaniste);
        }

        [TestMethod]
        public async Task PostBotaniste_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var botaniste = new Botaniste { IdUtilisateur = 1 };
            var mockSet = new Mock<DbSet<Botaniste>>();
            mockSet.Setup(m => m.Add(botaniste)).Verifiable();

            _mockContext.Setup(c => c.Botanistes).Returns(mockSet.Object);

            // Act
            var result = await _controller.PostBotaniste(botaniste);

            // Assert
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            createdAtActionResult.Should().NotBeNull();
            createdAtActionResult.StatusCode.Should().Be(201);
            createdAtActionResult.Value.Should().BeEquivalentTo(botaniste);
            mockSet.Verify(m => m.Add(botaniste), Times.Once());
        }

        [TestMethod]
        public async Task DeleteBotaniste_ReturnsNoContentResult()
        {
            // Arrange
            var botaniste = new Botaniste { IdUtilisateur = 1 };
            var mockSet = new Mock<DbSet<Botaniste>>();
            mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(botaniste);
            mockSet.Setup(m => m.Remove(botaniste)).Verifiable();

            _mockContext.Setup(c => c.Botanistes).Returns(mockSet.Object);

            // Act
            var result = await _controller.DeleteBotaniste(1);

            // Assert
            result.Should().BeOfType<NoContentResult>();
            mockSet.Verify(m => m.Remove(It.IsAny<Botaniste>()), Times.Once());
        }

        [TestMethod]
        public async Task PutBotaniste_ReturnsNoContentResult()
        {
            // Arrange
            var botaniste = new Botaniste { IdUtilisateur = 1 };
            var mockSet = new Mock<DbSet<Botaniste>>();
            mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(botaniste);

            _mockContext.Setup(c => c.Botanistes).Returns(mockSet.Object);
            _mockContext.Setup(c => c.Entry(botaniste).State).Returns(EntityState.Modified);

            // Act
            var result = await _controller.PutBotaniste(1, botaniste);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }
    }
}
