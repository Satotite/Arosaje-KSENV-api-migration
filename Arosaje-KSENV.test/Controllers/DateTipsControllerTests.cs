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
    public class DateTipsControllerTests
    {
        private Mock<ArosajeKsenvContext> _mockContext;
        private DateTipsController _controller;

        [TestInitialize]
        public void Initialize()
        {
            _mockContext = new Mock<ArosajeKsenvContext>();
            _controller = new DateTipsController(_mockContext.Object);
        }

        [TestMethod]
        public async Task GetDateTips_ReturnsOkResult_WithListOfDateTips()
        {
            // Arrange
            var dateTips = new List<DateTip>
            {
                new DateTip { IdTips = 1 },
                new DateTip { IdTips = 2 }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<DateTip>>();
            mockSet.As<IQueryable<DateTip>>().Setup(m => m.Provider).Returns(dateTips.Provider);
            mockSet.As<IQueryable<DateTip>>().Setup(m => m.Expression).Returns(dateTips.Expression);
            mockSet.As<IQueryable<DateTip>>().Setup(m => m.ElementType).Returns(dateTips.ElementType);
            mockSet.As<IQueryable<DateTip>>().Setup(m => m.GetEnumerator()).Returns(dateTips.GetEnumerator());

            _mockContext.Setup(c => c.DateTips).Returns(mockSet.Object);

            // Act
            var result = await _controller.GetDateTips();

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            var returnDateTips = okResult.Value as IEnumerable<DateTip>;
            returnDateTips.Should().NotBeNull();
            returnDateTips.Count().Should().Be(2);
        }

        [TestMethod]
        public async Task GetDateTip_ReturnsNotFound_WhenDateTipNotFound()
        {
            // Arrange
            var mockSet = new Mock<DbSet<DateTip>>();
            _mockContext.Setup(c => c.DateTips).Returns(mockSet.Object);

            // Act
            var result = await _controller.GetDateTip(1);

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public async Task GetDateTip_ReturnsOkResult_WithDateTip()
        {
            // Arrange
            var dateTip = new DateTip { IdTips = 1 };
            var mockSet = new Mock<DbSet<DateTip>>();
            mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(dateTip);

            _mockContext.Setup(c => c.DateTips).Returns(mockSet.Object);

            // Act
            var result = await _controller.GetDateTip(1);

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(dateTip);
        }

        [TestMethod]
        public async Task PostDateTip_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var dateTip = new DateTip { IdTips = 1 };
            var mockSet = new Mock<DbSet<DateTip>>();

            _mockContext.Setup(c => c.DateTips).Returns(mockSet.Object);

            // Act
            var result = await _controller.PostDateTip(dateTip);

            // Assert
            var createdAtActionResult = result.Result as CreatedAtActionResult;
            createdAtActionResult.Should().NotBeNull();
            createdAtActionResult.StatusCode.Should().Be(201);
            createdAtActionResult.Value.Should().BeEquivalentTo(dateTip);
        }

        [TestMethod]
        public async Task DeleteDateTip_ReturnsNoContentResult()
        {
            // Arrange
            var dateTip = new DateTip { IdTips = 1 };
            var mockSet = new Mock<DbSet<DateTip>>();
            mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(dateTip);
            mockSet.Setup(m => m.Remove(dateTip));

            _mockContext.Setup(c => c.DateTips).Returns(mockSet.Object);

            // Act
            var result = await _controller.DeleteDateTip(1);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [TestMethod]
        public async Task PutDateTip_ReturnsNoContentResult()
        {
            // Arrange
            var dateTip = new DateTip { IdTips = 1 };
            var mockSet = new Mock<DbSet<DateTip>>();
            mockSet.Setup(m => m.FindAsync(1)).ReturnsAsync(dateTip);

            _mockContext.Setup(c => c.DateTips).Returns(mockSet.Object);
            _mockContext.Setup(c => c.Entry(dateTip).State == EntityState.Modified);

            // Act
            var result = await _controller.PutDateTip(1, dateTip);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }
    }
}
