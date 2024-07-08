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
    public class EnvoyerRecevoirsControllerTests
    {
        private Mock<ArosajeKsenvContext> _mockContext;
        private EnvoyerRecevoirsController _controller;

        [TestInitialize]
        public void Initialize()
        {
            _mockContext = new Mock<ArosajeKsenvContext>();
            _controller = new EnvoyerRecevoirsController(_mockContext.Object);
        }

        [TestMethod]
        public async Task GetEnvoyerRecevoirs_ReturnsOkResult_WithListOfEnvoyerRecevoirs()
        {
            // Arrange
            var envoyerRecevoirs = new List<EnvoyerRecevoir>
            {
                new EnvoyerRecevoir { IdUtilisateur = 1 },
                new EnvoyerRecevoir { IdUtilisateur = 2 }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<EnvoyerRecevoir>>();
            mockSet.As<IQueryable<EnvoyerRecevoir>>().Setup(m => m.Provider).Returns(envoyerRecevoirs.Provider);
            mockSet.As<IQueryable<EnvoyerRecevoir>>().Setup(m => m.Expression).Returns(envoyerRecevoirs.Expression);
            mockSet.As<IQueryable<EnvoyerRecevoir>>().Setup(m => m.ElementType).Returns(envoyerRecevoirs.ElementType);
            mockSet.As<IQueryable<EnvoyerRecevoir>>().Setup(m => m.GetEnumerator()).Returns(envoyerRecevoirs.GetEnumerator());

            _mockContext.Setup(c => c.EnvoyerRecevoirs).Returns(mockSet.Object);

            // Act
            var result = await _controller.GetEnvoyerRecevoirs();

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            var returnEnvoyerRecevoirs = okResult.Value as IEnumerable<EnvoyerRecevoir>;
            returnEnvoyerRecevoirs.Should().NotBeNull();
            returnEnvoyerRecevoirs.Count().Should().Be(2);
        }

        [TestMethod]
        public async Task GetEnvoyerRecevoir_ReturnsNotFound_WhenEnvoyerRecevoirNotFound()
        {
            // Arrange
            var mockSet = new Mock<DbSet<EnvoyerRecevoir>>();
            _mockContext.Setup(c => c.EnvoyerRecevoirs).Returns(mockSet.Object);

            // Act
            var result = await _controller.GetEnvoyerRecevoir(1);

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public async Task GetEnvoyerRecevoir_ReturnsOkResult_WithEnvoyerRecevoir()
        {
            // Arrange
            var envoyerRecevoir = new EnvoyerRecevoir { IdUtilisateur = 1 };
            var mockSet = new Mock<DbSet<EnvoyerRecevoir>>();
            mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(envoyerRecevoir);

            _mockContext.Setup(c => c.EnvoyerRecevoirs).Returns(mockSet.Object);

            // Act
            var result = await _controller.GetEnvoyerRecevoir(1);

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(envoyerRecevoir);
        }

        [TestMethod]
        public async Task PostEnvoyerRecevoir_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var envoyerRecevoir = new EnvoyerRecevoir { IdUtilisateur = 1 };
            var mockSet = new Mock<DbSet<EnvoyerRecevoir>>();

            _mockContext.Setup(c => c.EnvoyerRecevoirs).Returns(mockSet.Object);

            // Act
            var result = await _controller.PostEnvoyerRecevoir(envoyerRecevoir);

            // Assert
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            createdAtActionResult.Should().NotBeNull();
            createdAtActionResult.StatusCode.Should().Be(201);
            createdAtActionResult.Value.Should().BeEquivalentTo(envoyerRecevoir);
        }

        [TestMethod]
        public async Task DeleteEnvoyerRecevoir_ReturnsNoContentResult()
        {
            // Arrange
            var envoyerRecevoir = new EnvoyerRecevoir { IdUtilisateur = 1 };
            var mockSet = new Mock<DbSet<EnvoyerRecevoir>>();
            mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(envoyerRecevoir);
            mockSet.Setup(m => m.Remove(envoyerRecevoir));

            _mockContext.Setup(c => c.EnvoyerRecevoirs).Returns(mockSet.Object);

            // Act
            var result = await _controller.DeleteEnvoyerRecevoir(1);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [TestMethod]
        public async Task PutEnvoyerRecevoir_ReturnsNoContentResult()
        {
            // Arrange
            var envoyerRecevoir = new EnvoyerRecevoir { IdUtilisateur = 1 };
            var mockSet = new Mock<DbSet<EnvoyerRecevoir>>();
            mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(envoyerRecevoir);

            _mockContext.Setup(c => c.EnvoyerRecevoirs).Returns(mockSet.Object);
            _mockContext.Setup(c => c.Entry(envoyerRecevoir).State == EntityState.Modified);

            // Act
            var result = await _controller.PutEnvoyerRecevoir(1, envoyerRecevoir);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }
    }
}
