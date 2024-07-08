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
    public class MembresControllerTests
    {
        private Mock<ArosajeKsenvContext> _mockContext;
        private MembresController _controller;

        [TestInitialize]
        public void Initialize()
        {
            _mockContext = new Mock<ArosajeKsenvContext>();
            _controller = new MembresController(_mockContext.Object);
        }

        [TestMethod]
        public async Task GetMembres_ReturnsOkResult_WithListOfMembres()
        {
            // Arrange
            var membres = new List<Membre>
            {
                new Membre { IdUtilisateur = 1 },
                new Membre { IdUtilisateur = 2 }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Membre>>();
            mockSet.As<IQueryable<Membre>>().Setup(m => m.Provider).Returns(membres.Provider);
            mockSet.As<IQueryable<Membre>>().Setup(m => m.Expression).Returns(membres.Expression);
            mockSet.As<IQueryable<Membre>>().Setup(m => m.ElementType).Returns(membres.ElementType);
            mockSet.As<IQueryable<Membre>>().Setup(m => m.GetEnumerator()).Returns(membres.GetEnumerator());

            _mockContext.Setup(c => c.Membres).Returns(mockSet.Object);

            // Act
            var result = await _controller.GetMembres();

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            var returnMembres = okResult.Value as IEnumerable<Membre>;
            returnMembres.Should().NotBeNull();
            returnMembres.Count().Should().Be(2);
        }

        [TestMethod]
        public async Task GetMembre_ReturnsNotFound_WhenMembreNotFound()
        {
            // Arrange
            var mockSet = new Mock<DbSet<Membre>>();
            _mockContext.Setup(c => c.Membres).Returns(mockSet.Object);

            // Act
            var result = await _controller.GetMembre(1);

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public async Task GetMembre_ReturnsOkResult_WithMembre()
        {
            // Arrange
            var membre = new Membre { IdUtilisateur = 1 };
            var mockSet = new Mock<DbSet<Membre>>();
            mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(membre);

            _mockContext.Setup(c => c.Membres).Returns(mockSet.Object);

            // Act
            var result = await _controller.GetMembre(1);

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(membre);
        }

        [TestMethod]
        public async Task PostMembre_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var membre = new Membre { IdUtilisateur = 1 };
            var mockSet = new Mock<DbSet<Membre>>();

            _mockContext.Setup(c => c.Membres).Returns(mockSet.Object);

            // Act
            var result = await _controller.PostMembre(membre);

            // Assert
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            createdAtActionResult.Should().NotBeNull();
            createdAtActionResult.StatusCode.Should().Be(201);
            createdAtActionResult.Value.Should().BeEquivalentTo(membre);
        }

        [TestMethod]
        public async Task DeleteMembre_ReturnsNoContentResult()
        {
            // Arrange
            var membre = new Membre { IdUtilisateur = 1 };
            var mockSet = new Mock<DbSet<Membre>>();
            mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(membre);
            mockSet.Setup(m => m.Remove(membre));

            _mockContext.Setup(c => c.Membres).Returns(mockSet.Object);

            // Act
            var result = await _controller.DeleteMembre(1);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [TestMethod]
        public async Task PutMembre_ReturnsNoContentResult()
        {
            // Arrange
            var membre = new Membre { IdUtilisateur = 1 };
            var mockSet = new Mock<DbSet<Membre>>();
            mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(membre);

            _mockContext.Setup(c => c.Membres).Returns(mockSet.Object);
            _mockContext.Setup(c => c.Entry(membre).State == EntityState.Modified);

            // Act
            var result = await _controller.PutMembre(1, membre);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }
    }
}
