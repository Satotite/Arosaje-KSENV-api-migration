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
    public class PhotosControllerTests
    {
        private Mock<ArosajeKsenvContext> _mockContext;
        private PhotosController _controller;

        [TestInitialize]
        public void Initialize()
        {
            _mockContext = new Mock<ArosajeKsenvContext>();
            _controller = new PhotosController(_mockContext.Object);
        }

        [TestMethod]
        public async Task GetPhotos_ReturnsOkResult_WithListOfPhotos()
        {
            // Arrange
            var photos = new List<Photo>
            {
                new Photo { IdPhoto = 1, Image = "image1.jpg", extension = "jpg" },
                new Photo { IdPhoto = 2, Image = "image2.png", extension = "png" }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Photo>>();
            mockSet.As<IQueryable<Photo>>().Setup(m => m.Provider).Returns(photos.Provider);
            mockSet.As<IQueryable<Photo>>().Setup(m => m.Expression).Returns(photos.Expression);
            mockSet.As<IQueryable<Photo>>().Setup(m => m.ElementType).Returns(photos.ElementType);
            mockSet.As<IQueryable<Photo>>().Setup(m => m.GetEnumerator()).Returns(photos.GetEnumerator());

            _mockContext.Setup(c => c.Photos).Returns(mockSet.Object);

            // Act
            var result = await _controller.GetPhotos();

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(photos);
        }

        [TestMethod]
        public async Task GetPhoto_ReturnsNotFound_WhenPhotoNotFound()
        {
            // Arrange
            var mockSet = new Mock<DbSet<Photo>>();
            _mockContext.Setup(c => c.Photos).Returns(mockSet.Object);

            // Act
            var result = await _controller.GetPhoto(1);

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public async Task GetPhoto_ReturnsOkResult_WithPhoto()
        {
            // Arrange
            var photo = new Photo { IdPhoto = 1, Image = "image1.jpg", extension = "jpg" };
            var mockSet = new Mock<DbSet<Photo>>();
            mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(photo);

            _mockContext.Setup(c => c.Photos).Returns(mockSet.Object);

            // Act
            var result = await _controller.GetPhoto(1);

            // Assert
            result.Result.Should().BeOfType<OkObjectResult>();
            var okResult = result.Result as OkObjectResult;
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(photo);
        }

        [TestMethod]
        public async Task PostPhoto_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var photo = new Photo { IdPhoto = 1, Image = "image1.jpg", extension = "jpg" };
            var mockSet = new Mock<DbSet<Photo>>();

            _mockContext.Setup(c => c.Photos).Returns(mockSet.Object);

            // Act
            var result = await _controller.PostPhoto(photo);

            // Assert
            result.Result.Should().BeOfType<CreatedAtActionResult>();
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            createdAtActionResult.StatusCode.Should().Be(201);
            createdAtActionResult.Value.Should().BeEquivalentTo(photo);
        }

        [TestMethod]
        public async Task DeletePhoto_ReturnsNoContentResult()
        {
            // Arrange
            var photo = new Photo { IdPhoto = 1, Image = "image1.jpg", extension = "jpg" };
            var mockSet = new Mock<DbSet<Photo>>();
            mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(photo);
            mockSet.Setup(m => m.Remove(photo));

            _mockContext.Setup(c => c.Photos).Returns(mockSet.Object);

            // Act
            var result = await _controller.DeletePhoto(1);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [TestMethod]
        public async Task PutPhoto_ReturnsNoContentResult()
        {
            // Arrange
            var photo = new Photo { IdPhoto = 1, Image = "image1.jpg", extension = "jpg" };
            var mockSet = new Mock<DbSet<Photo>>();
            mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(photo);

            _mockContext.Setup(c => c.Photos).Returns(mockSet.Object);
            _mockContext.Setup(c => c.Entry(photo).State == EntityState.Modified);


            // Act
            var result = await _controller.PutPhoto(1, photo);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }
    }
}
