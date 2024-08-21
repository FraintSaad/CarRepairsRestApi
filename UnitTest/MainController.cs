
using CarRepairsRestApi.Controllers;
using CarRepairsRestApi.Models;
using CarRepairsRestApi.Repositories.Interfaces;
using CarRepairsRestApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    public class MainControllerTests
    {
        // Тест для метода Get() контроллера
        [Fact]
        public void GetDataMessage()
        {
            // Подготовка данных для мокирования
            var mockDocs = new Mock<IBaseRepository<Document>>();
            var mockService = new Mock<IRepairService>();
            var document = GetDoc();
            mockDocs.Setup(x => x.GetAll()).Returns(new List<Document> { document });

            // Создание экземпляра контроллера с мокированными сервисами
            MainController controller = new MainController(mockService.Object, mockDocs.Object);

            // Вызов метода Get() контроллера
            JsonResult result = controller.Get() as JsonResult;

            // Проверка результата
            Assert.Equal(new List<Document> { document }, result?.Value);
        }

        // Тест для проверки, что Get() возвращает не null
        [Fact]
        public void GetNotNull()
        {
            // Подготовка мокированных сервисов
            var mockDocs = new Mock<IBaseRepository<Document>>();
            var mockService = new Mock<IRepairService>();
            mockDocs.Setup(x => x.Create(GetDoc())).Returns(GetDoc());

            // Создание экземпляра контроллера
            MainController controller = new MainController(mockService.Object, mockDocs.Object);

            // Вызов метода Get()
            JsonResult result = controller.Get() as JsonResult;

            // Проверка результата
            Assert.NotNull(result);
        }

        // Тест для метода Post() контроллера
        [Fact]
        public void PostDataMessage()
        {
            // Подготовка мокированных сервисов
            var mockDocs = new Mock<IBaseRepository<Document>>();
            var mockService = new Mock<IRepairService>();
            mockDocs.Setup(x => x.Create(GetDoc())).Returns(GetDoc());

            // Создание экземпляра контроллера
            MainController controller = new MainController(mockService.Object, mockDocs.Object);

            // Вызов метода Post()
            JsonResult result = controller.Post() as JsonResult;

            // Проверка результата
            Assert.Equal("Work was successfully done", result?.Value);
        }

        // Тест для метода Put() контроллера
        [Fact]
        public void UpdateDataMessage()
        {
            // Подготовка данных для мокирования
            var mockDocs = new Mock<IBaseRepository<Document>>();
            var mockService = new Mock<IRepairService>();
            var document = GetDoc();
            mockDocs.Setup(x => x.Get(document.Id)).Returns(document);
            mockDocs.Setup(x => x.Update(document)).Returns(document);

            // Создание экземпляра контроллера
            MainController controller = new MainController(mockService.Object, mockDocs.Object);

            // Вызов метода Put()
            JsonResult result = controller.Put(document) as JsonResult;

            // Проверка результата
            Assert.Equal($"Update successful {document.Id}", result?.Value);
        }

        // Тест для метода Delete() контроллера
        [Fact]
        public void DeleteDataMessage()
        {
            // Подготовка данных для мокирования
            var mockDocs = new Mock<IBaseRepository<Document>>();
            var mockService = new Mock<IRepairService>();
            var doc = GetDoc();
            mockDocs.Setup(x => x.Get(doc.Id)).Returns(doc);
            mockDocs.Setup(x => x.Delete(doc.Id));

            // Создание экземпляра контроллера
            MainController controller = new MainController(mockService.Object, mockDocs.Object);

            // Вызов метода Delete()
            JsonResult result = controller.Delete(doc.Id) as JsonResult;

            // Проверка результата
            Assert.Equal("Delete successful", result?.Value);
        }

        // Метод для создания тестового документа
        public Document GetDoc()
        {
            // Создание мокированных репозиториев для автомобилей и работников
            var mockCars = new Mock<IBaseRepository<Car>>();
            var mockWorkers = new Mock<IBaseRepository<Worker>>();

            // Генерация случайных ID для автомобиля и работника
            var carId = Guid.NewGuid();
            var workerId = Guid.NewGuid();

            // Настройка мокированных репозиториев для создания тестовых данных
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

            // Возвращение тестового документа
            return new Document
            {
                Id = Guid.NewGuid(),
                CarId = carId,
                WorkerId = workerId
            };
        }
    }
}
