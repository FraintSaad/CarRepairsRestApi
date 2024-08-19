using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Xunit;
namespace UnitTests
{
    [TestClass]
    public class MainControllerTests
    {
        [Fact]
        public void GetDataMessage()
        {
            var mockDocs = new Mock<IBaseRepository<Document>>();
            var mockService = new Mock<IRepairService>();
            var document = GetDoc();
            mockDocs.Setup(x => x.GetAll()).Returns(new List<Document> { document });

            // Arrange
            MainController controller = new MainController(mockService.Object, mockDocs.Object);

            // Act
            JsonResult result = controller.Get() as JsonResult;

            // Assert
            Assert.Equal(new List<Document> { document }, result?.Value);
        }

        [Fact]
        public void GetNotNull()
        {
            var mockDocs = new Mock<IBaseRepository<Document>>();
            var mockService = new Mock<IRepairService>();
            mockDocs.Setup(x => x.Create(GetDoc())).Returns(GetDoc());

            // Arrange
            MainController controller = new MainController(mockService.Object, mockDocs.Object);
            // Act
            JsonResult result = controller.Get() as JsonResult;
            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void PostDataMessage()
        {
            var mockDocs = new Mock<IBaseRepository<Document>>();
            var mockService = new Mock<IRepairService>();
            mockDocs.Setup(x => x.Create(GetDoc())).Returns(GetDoc());

            // Arrange
            MainController controller = new MainController(mockService.Object, mockDocs.Object);

            // Act
            JsonResult result = controller.Post() as JsonResult;

            // Assert
            Assert.Equal("Work was successfully done", result?.Value);
        }

        [Fact]
        public void UpdateDataMessage()
        {
            var mockDocs = new Mock<IBaseRepository<Document>>();
            var mockService = new Mock<IRepairService>();
            var document = GetDoc();

            mockDocs.Setup(x => x.Get(document.Id)).Returns(document);
            mockDocs.Setup(x => x.Update(document)).Returns(document);

            // Arrange
            MainController controller = new MainController(mockService.Object, mockDocs.Object);

            // Act
            JsonResult result = controller.Put(document) as JsonResult;

            // Assert
            Assert.Equal($"Update successful {document.Id}", result?.Value);
        }

        [Fact]
        public void DeleteDataMessage()
        {
            var mockDocs = new Mock<IBaseRepository<Document>>();
            var mockService = new Mock<IRepairService>();
            var doc = GetDoc();

            mockDocs.Setup(x => x.Get(doc.Id)).Returns(doc);
            mockDocs.Setup(x => x.Delete(doc.Id));

            // Arrange
            MainController controller = new MainController(mockService.Object, mockDocs.Object);

            // Act
            JsonResult result = controller.Delete(doc.Id) as JsonResult;

            // Assert
            Assert.Equal("Delete successful", result?.Value);
        }

        public Document GetDoc()
        {
            var mockCars = new Mock<IBaseRepository<Car>>();
            var mockWorkers = new Mock<IBaseRepository<Worker>>();

            var carId = Guid.NewGuid();
            var workerId = Guid.NewGuid();
            mockCars.Setup(x => x.Create(new Car()
            {
                Id = carId,
                Name = "car",
                Number = "123"
            }));

            mockWorkers.Setup(x => x.Create(new Worker()
            {
                Id = workerId,
                Name = "worker",
                Position = "manager",
                Telephone = "89165555555"
            }));

            return new Document
            {
                Id = Guid.NewGuid(),
                CarId = carId,
                WorkerId = workerId
            };
        }
    }
}
