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
    public class PlantesControllerTests
    {
        private Mock<ArosajeKsenvContext> _mockContext;
        private PlantesController _controller;

        [TestInitialize]
        public void Initialize()
        {
            _mockContext = new Mock<ArosajeKsenvContext>();
            _controller = new PlantesController(_mockContext.Object);
        }

        [TestMethod]
        public async Task GetPlantes_ReturnsOkResult_WithListOfPlantes()
        {
            // Arrange
            var plantes = new List<Plante>
            {
                new Plante { IdPlante = 1, Nom = "Rose", Espece = "Rosa" },
                new Plante { IdPlante = 2, Nom = "Tulip", Espece = "Tulipa" }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Plante>>();
            mockSet.As<IQueryable<Plante>>().Setup(m => m.Provider).Returns(plantes.Provider);
            mockSet.As<IQueryable<Plante>>().Setup(m => m.Expression).Returns(plantes.Expression);
            mockSet.As<IQueryable<Plante>>().Setup(m => m.ElementType).Returns(plantes.ElementType);
            mockSet.As<IQueryable<Plante>>().Setup(m => m.GetEnumerator()).Returns(plantes.GetEnumerator());

            _mockContext.Setup(c => c.Plantes).Returns(mockSet.Object);

            // Act
            var result = await _controller.GetPlantes();

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(plantes);
        }

        [TestMethod]
        public async Task GetPlante_ReturnsNotFound_WhenPlanteNotFound()
        {
            // Arrange
            var mockSet = new Mock<DbSet<Plante>>();
            _mockContext.Setup(c => c.Plantes).Returns(mockSet.Object);

            // Act
            var result = await _controller.GetPlante(1);

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public async Task GetPlante_ReturnsOkResult_WithPlante()
        {
            // Arrange
            var plante = new Plante { IdPlante = 1, Nom = "Rose", Espece = "Rosa" };
            var mockSet = new Mock<DbSet<Plante>>();
            mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(plante);

            _mockContext.Setup(c => c.Plantes).Returns(mockSet.Object);

            // Act
            var result = await _controller.GetPlante(1);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(plante);
        }

        [TestMethod]
        public async Task GetPlanteFromName_ReturnsNotFound_WhenPlanteNotFound()
        {
            // Arrange
            var mockSet = new Mock<DbSet<Plante>>();
            _mockContext.Setup(c => c.Plantes).Returns(mockSet.Object);

            // Act
            var result = await _controller.GetPlanteFromName("Rose");

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public async Task GetPlanteFromName_ReturnsOkResult_WithPlante()
        {
            // Arrange
            var plante = new Plante { IdPlante = 1, Nom = "Rose", Espece = "Rosa" };
            var plantes = new List<Plante> { plante }.AsQueryable();

            var mockSet = new Mock<DbSet<Plante>>();
            mockSet.As<IQueryable<Plante>>().Setup(m => m.Provider).Returns(plantes.Provider);
            mockSet.As<IQueryable<Plante>>().Setup(m => m.Expression).Returns(plantes.Expression);
            mockSet.As<IQueryable<Plante>>().Setup(m => m.ElementType).Returns(plantes.ElementType);
            mockSet.As<IQueryable<Plante>>().Setup(m => m.GetEnumerator()).Returns(plantes.GetEnumerator());

            _mockContext.Setup(c => c.Plantes).Returns(mockSet.Object);

            // Act
            var result = await _controller.GetPlanteFromName("Rose");

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(plante);
        }

        [TestMethod]
        public async Task PostPlante_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var plante = new Plante { IdPlante = 1, Nom = "Rose", Espece = "Rosa" };
            var mockSet = new Mock<DbSet<Plante>>();

            _mockContext.Setup(c => c.Plantes).Returns(mockSet.Object);

            // Act
            var result = await _controller.PostPlante(plante);

            // Assert
            result.Result.Should().BeOfType<CreatedAtActionResult>();
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            createdAtActionResult.StatusCode.Should().Be(201);
            createdAtActionResult.Value.Should().BeEquivalentTo(plante);
        }

        [TestMethod]
        public async Task DeletePlante_ReturnsNoContentResult()
        {
            // Arrange
            var plante = new Plante { IdPlante = 1, Nom = "Rose", Espece = "Rosa" };
            var mockSet = new Mock<DbSet<Plante>>();
            mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(plante);
            mockSet.Setup(m => m.Remove(plante));

            _mockContext.Setup(c => c.Plantes).Returns(mockSet.Object);

            // Act
            var result = await _controller.DeletePlante(1);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [TestMethod]
        public async Task PutPlante_ReturnsNoContentResult()
        {
            // Arrange
            var plante = new Plante { IdPlante = 1, Nom = "Rose", Espece = "Rosa" };
            var mockSet = new Mock<DbSet<Plante>>();
            mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(plante);

            _mockContext.Setup(c => c.Plantes).Returns(mockSet.Object);
            _mockContext.Setup(c => c.Entry(plante).State == EntityState.Modified);

            // Act
            var result = await _controller.PutPlante(1, plante);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }
    }
}
