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
    public class UtilisateursControllerTests
    {
        private Mock<ArosajeKsenvContext> _mockContext;
        private UtilisateursController _controller;

        [TestInitialize]
        public void Initialize()
        {
            _mockContext = new Mock<ArosajeKsenvContext>();
            _controller = new UtilisateursController(_mockContext.Object);
        }

        [TestMethod]
        public async Task GetUtilisateurs_ReturnsOkResult_WithListOfUtilisateurs()
        {
            // Arrange
            var utilisateurs = new List<Utilisateur>
            {
                new Utilisateur { IdUtilisateur = 1, Nom = "Doe", Prenom = "John" },
                new Utilisateur { IdUtilisateur = 2, Nom = "Smith", Prenom = "Jane" }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Utilisateur>>();
            mockSet.As<IQueryable<Utilisateur>>().Setup(m => m.Provider).Returns(utilisateurs.Provider);
            mockSet.As<IQueryable<Utilisateur>>().Setup(m => m.Expression).Returns(utilisateurs.Expression);
            mockSet.As<IQueryable<Utilisateur>>().Setup(m => m.ElementType).Returns(utilisateurs.ElementType);
            mockSet.As<IQueryable<Utilisateur>>().Setup(m => m.GetEnumerator()).Returns(utilisateurs.GetEnumerator());

            _mockContext.Setup(c => c.Utilisateurs).Returns(mockSet.Object);

            // Act
            var result = await _controller.GetUtilisateurs();

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(utilisateurs);
        }

        [TestMethod]
        public async Task GetUtilisateur_ReturnsNotFound_WhenUtilisateurNotFound()
        {
            // Arrange
            var mockSet = new Mock<DbSet<Utilisateur>>();
            _mockContext.Setup(c => c.Utilisateurs).Returns(mockSet.Object);

            // Act
            var result = await _controller.GetUtilisateur(1);

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public async Task GetUtilisateur_ReturnsOkResult_WithUtilisateur()
        {
            // Arrange
            var utilisateur = new Utilisateur { IdUtilisateur = 1, Nom = "Doe", Prenom = "John" };
            var mockSet = new Mock<DbSet<Utilisateur>>();
            mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(utilisateur);

            _mockContext.Setup(c => c.Utilisateurs).Returns(mockSet.Object);

            // Act
            var result = await _controller.GetUtilisateur(1);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(utilisateur);
        }

        [TestMethod]
        public async Task GetUtilisateurFromMail_ReturnsNotFound_WhenUtilisateurNotFound()
        {
            // Arrange
            var mockSet = new Mock<DbSet<Utilisateur>>();
            _mockContext.Setup(c => c.Utilisateurs).Returns(mockSet.Object);

            // Act
            var result = await _controller.GetUtilisateurFromMail("john.doe@example.com");

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public async Task GetUtilisateurFromMail_ReturnsOkResult_WithUtilisateur()
        {
            // Arrange
            var utilisateur = new Utilisateur { IdUtilisateur = 1, Nom = "Doe", Prenom = "John", Email = "john.doe@example.com" };
            var utilisateurs = new List<Utilisateur> { utilisateur }.AsQueryable();

            var mockSet = new Mock<DbSet<Utilisateur>>();
            mockSet.As<IQueryable<Utilisateur>>().Setup(m => m.Provider).Returns(utilisateurs.Provider);
            mockSet.As<IQueryable<Utilisateur>>().Setup(m => m.Expression).Returns(utilisateurs.Expression);
            mockSet.As<IQueryable<Utilisateur>>().Setup(m => m.ElementType).Returns(utilisateurs.ElementType);
            mockSet.As<IQueryable<Utilisateur>>().Setup(m => m.GetEnumerator()).Returns(utilisateurs.GetEnumerator());

            _mockContext.Setup(c => c.Utilisateurs).Returns(mockSet.Object);

            // Act
            var result = await _controller.GetUtilisateurFromMail("john-doe@example-com");

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(utilisateur);
        }

        [TestMethod]
        public async Task PostUtilisateur_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var utilisateur = new Utilisateur { IdUtilisateur = 1, Nom = "Doe", Prenom = "John" };
            var mockSet = new Mock<DbSet<Utilisateur>>();

            _mockContext.Setup(c => c.Utilisateurs).Returns(mockSet.Object);

            // Act
            var result = await _controller.PostUtilisateur(utilisateur);

            // Assert
            result.Result.Should().BeOfType<CreatedAtActionResult>();
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            createdAtActionResult.StatusCode.Should().Be(201);
            createdAtActionResult.Value.Should().BeEquivalentTo(utilisateur);
        }

        [TestMethod]
        public async Task DeleteUtilisateur_ReturnsNoContentResult()
        {
            // Arrange
            var utilisateur = new Utilisateur { IdUtilisateur = 1, Nom = "Doe", Prenom = "John" };
            var mockSet = new Mock<DbSet<Utilisateur>>();
            mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(utilisateur);
            mockSet.Setup(m => m.Remove(utilisateur));

            _mockContext.Setup(c => c.Utilisateurs).Returns(mockSet.Object);

            // Act
            var result = await _controller.DeleteUtilisateur(1);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [TestMethod]
        public async Task PutUtilisateur_ReturnsNoContentResult()
        {
            // Arrange
            var utilisateur = new Utilisateur { IdUtilisateur = 1, Nom = "Doe", Prenom = "John" };
            var mockSet = new Mock<DbSet<Utilisateur>>();
            mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(utilisateur);

            _mockContext.Setup(c => c.Utilisateurs).Returns(mockSet.Object);
            _mockContext.Setup(c => c.Entry(utilisateur).State == EntityState.Modified);

            // Act
            var result = await _controller.PutUtilisateur(1, utilisateur);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }
    }
}
