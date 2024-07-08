using System;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Moq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Arosaje_KSENV.Controllers;
using Arosaje_KSENV.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ArosajeKserv.Tests.Controllers
{
    [TestClass]
    public class TokenControllerTests
    {
        private Mock<ArosajeKsenvContext> _mockContext;
        private Mock<IConfiguration> _mockConfig;
        private TokenController _controller;

        [TestInitialize]
        public void Initialize()
        {
            _mockContext = new Mock<ArosajeKsenvContext>();
            _mockConfig = new Mock<IConfiguration>();

            _mockConfig.Setup(c => c["Jwt:Key"]).Returns("ThisIsASecretKeyForJwtToken");
            _mockConfig.Setup(c => c["Jwt:Issuer"]).Returns("TestIssuer");

            _controller = new TokenController(_mockConfig.Object, _mockContext.Object);
        }

        [TestMethod]
        public void Post_ReturnsOkResult_WithToken_WhenCredentialsAreValid()
        {
            // Arrange
            var loginRequest = new LoginRequest { email = "test@example.com", mdp = "password" };
            var user = new Utilisateur { Email = "test@example.com", Mdp = "password" };

            var utilisateurs = new List<Utilisateur> { user }.AsQueryable();
            var mockSet = new Mock<DbSet<Utilisateur>>();
            mockSet.As<IQueryable<Utilisateur>>().Setup(m => m.Provider).Returns(utilisateurs.Provider);
            mockSet.As<IQueryable<Utilisateur>>().Setup(m => m.Expression).Returns(utilisateurs.Expression);
            mockSet.As<IQueryable<Utilisateur>>().Setup(m => m.ElementType).Returns(utilisateurs.ElementType);
            mockSet.As<IQueryable<Utilisateur>>().Setup(m => m.GetEnumerator()).Returns(utilisateurs.GetEnumerator());

            _mockContext.Setup(c => c.Utilisateurs).Returns(mockSet.Object);

            // Act
            var result = _controller.Post(loginRequest);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeOfType<string>();

            var token = okResult.Value as string;
            token.Should().NotBeNullOrEmpty();

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
            jsonToken.Issuer.Should().Be("TestIssuer");
        }

        [TestMethod]
        public void Post_ReturnsBadRequest_WhenCredentialsAreInvalid()
        {
            // Arrange
            var loginRequest = new LoginRequest { email = "test@example.com", mdp = "wrongpassword" };

            var utilisateurs = new List<Utilisateur>().AsQueryable();
            var mockSet = new Mock<DbSet<Utilisateur>>();
            mockSet.As<IQueryable<Utilisateur>>().Setup(m => m.Provider).Returns(utilisateurs.Provider);
            mockSet.As<IQueryable<Utilisateur>>().Setup(m => m.Expression).Returns(utilisateurs.Expression);
            mockSet.As<IQueryable<Utilisateur>>().Setup(m => m.ElementType).Returns(utilisateurs.ElementType);
            mockSet.As<IQueryable<Utilisateur>>().Setup(m => m.GetEnumerator()).Returns(utilisateurs.GetEnumerator());

            _mockContext.Setup(c => c.Utilisateurs).Returns(mockSet.Object);

            // Act
            var result = _controller.Post(loginRequest);

            // Assert
            result.Should().BeOfType<BadRequestResult>();
            var badRequestResult = result as BadRequestResult;
            badRequestResult.StatusCode.Should().Be(400);
        }
    }
}
