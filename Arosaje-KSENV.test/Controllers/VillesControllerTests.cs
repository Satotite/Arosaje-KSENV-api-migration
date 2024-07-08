using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using FluentAssertions;
using Arosaje_KSENV.Controllers;
using Arosaje_KSENV.Models;

namespace Arosaje_KSENV.Tests.Controllers
{
    public class VillesControllerTests
    {
        private Mock<ArosajeKsenvContext> _mockContext;
        private VillesController _controller;

        public VillesControllerTests()
        {
            _mockContext = new Mock<ArosajeKsenvContext>();
            _controller = new VillesController(_mockContext.Object);
        }

        [TestMethod]
        public async Task GetVilles_ReturnsOkResult_WithListOfVilles()
        {
            // Arrange
            var villes = new List<Ville>
            {
                new Ville { IdVille = 1, Nom = "Paris" },
                new Ville { IdVille = 2, Nom = "Berlin" }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Ville>>();
            mockSet.As<IQueryable<Ville>>().Setup(m => m.Provider).Returns(villes.Provider);
            mockSet.As<IQueryable<Ville>>().Setup(m => m.Expression).Returns(villes.Expression);
            mockSet.As<IQueryable<Ville>>().Setup(m => m.ElementType).Returns(villes.ElementType);
            mockSet.As<IQueryable<Ville>>().Setup(m => m.GetEnumerator()).Returns(villes.GetEnumerator());

            _mockContext.Setup(c => c.Villes).Returns(mockSet.Object);

            // Act
            var result = await _controller.GetVilles();

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(villes);
        }

        [TestMethod]
        public async Task GetVille_ReturnsNotFound_WhenVilleNotFound()
        {
            // Arrange
            var mockSet = new Mock<DbSet<Ville>>();
            _mockContext.Setup(c => c.Villes).Returns(mockSet.Object);

            // Act
            var result = await _controller.GetVille(1);

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public async Task GetVille_ReturnsOkResult_WithVille()
        {
            // Arrange
            var ville = new Ville { IdVille = 1, Nom = "Paris" };
            var mockSet = new Mock<DbSet<Ville>>();
            mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(ville);

            _mockContext.Setup(c => c.Villes).Returns(mockSet.Object);

            // Act
            var result = await _controller.GetVille(1);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(ville);
        }

        [TestMethod]
        public async Task GetVilleFromName_ReturnsNotFound_WhenVilleNotFound()
        {
            // Arrange
            var mockSet = new Mock<DbSet<Ville>>();
            _mockContext.Setup(c => c.Villes).Returns(mockSet.Object);

            // Act
            var result = await _controller.GetVilleFromName("paris");

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public async Task GetVilleFromName_ReturnsOkResult_WithVille()
        {
            // Arrange
            var ville = new Ville { IdVille = 1, Nom = "Paris" };
            var villes = new List<Ville> { ville }.AsQueryable();

            var mockSet = new Mock<DbSet<Ville>>();
            mockSet.As<IQueryable<Ville>>().Setup(m => m.Provider).Returns(villes.Provider);
            mockSet.As<IQueryable<Ville>>().Setup(m => m.Expression).Returns(villes.Expression);
            mockSet.As<IQueryable<Ville>>().Setup(m => m.ElementType).Returns(villes.ElementType);
            mockSet.As<IQueryable<Ville>>().Setup(m => m.GetEnumerator()).Returns(villes.GetEnumerator());

            _mockContext.Setup(c => c.Villes).Returns(mockSet.Object);

            // Act
            var result = await _controller.GetVilleFromName("paris");

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(ville);
        }

        [TestMethod]
        public async Task PostVille_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var ville = new Ville { IdVille = 1, Nom = "Paris" };
            var mockSet = new Mock<DbSet<Ville>>();

            _mockContext.Setup(c => c.Villes).Returns(mockSet.Object);

            // Act
            var result = await _controller.PostVille(ville);

            // Assert
            result.Result.Should().BeOfType<CreatedAtActionResult>();
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            createdAtActionResult.StatusCode.Should().Be(201);
            createdAtActionResult.Value.Should().BeEquivalentTo(ville);
        }

        [TestMethod]
        public async Task DeleteVille_ReturnsNoContentResult()
        {
            // Arrange
            var ville = new Ville { IdVille = 1, Nom = "Paris" };
            var mockSet = new Mock<DbSet<Ville>>();
            mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(ville);
            mockSet.Setup(m => m.Remove(ville));

            _mockContext.Setup(c => c.Villes).Returns(mockSet.Object);

            // Act
            var result = await _controller.DeleteVille(1);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [TestMethod]
        public async Task PutVille_ReturnsNoContentResult()
        {
            // Arrange
            var ville = new Ville { IdVille = 1, Nom = "Paris" };
            var mockSet = new Mock<DbSet<Ville>>();
            mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(ville);

            _mockContext.Setup(c => c.Villes).Returns(mockSet.Object);
            _mockContext.Setup(c => c.Entry(ville).State == EntityState.Modified);

            // Act
            var result = await _controller.PutVille(1, ville);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }
    }
}
