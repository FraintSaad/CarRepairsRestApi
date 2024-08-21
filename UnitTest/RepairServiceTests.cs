using CarRepairsRestApi.Models;
using CarRepairsRestApi.Repositories.Interfaces;
using CarRepairsRestApi.Services.Implimentations;
using CarRepairsRestApi.Services.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    public class RepairServiceTests
    {
        [Fact]
        public void WorkSuccessTest()
        {
            // Создаем моки для репозиториев
            var mockCars = new Mock<IBaseRepository<Car>>();
            var mockWorkers = new Mock<IBaseRepository<Worker>>();
            var mockDocs = new Mock<IBaseRepository<Document>>();

            // Создаем тестовые данные 
            var carId = Guid.NewGuid();
            var workerId = Guid.NewGuid();
            var car = CreateCar(carId);
            var worker = CreateWorker(workerId);
            var doc = CreateDoc(Guid.NewGuid(), workerId, carId);

            // Настраиваем моки для возврата тестовых данных
            mockCars.Setup(x => x.Create(car)).Returns(car);
            mockDocs.Setup(x => x.Create(doc)).Returns(doc);
            mockWorkers.Setup(x => x.Create(worker)).Returns(worker);

            // Создаем экземпляр сервиса с мокированными репозиториями
            var service = new RepairService(mockDocs.Object, mockCars.Object, mockWorkers.Object);

            // Вызываем метод Work()
            service.Work();

            // Проверяем, что метод Work() был вызван
            mockCars.Verify(x => x.Create(car), Times.Once);
            mockDocs.Verify(x => x.Create(doc), Times.Once);
            mockWorkers.Verify(x => x.Create(worker), Times.Once);
        }

        // Методы для создания тестовых данных
        private Car CreateCar(Guid carId)
        {
            return new Car()
            {
                Id = carId,
                Name = "car",
                Number = "123"
            };
        }

        private Worker CreateWorker(Guid workerId)
        {
            return new Worker()
            {
                Id = workerId,
                Name = "worker",
                Position = "manager",
                Telephone = "89165555555"
            };
        }

        private Document CreateDoc(Guid docId, Guid workerId, Guid carId)
        {
            return new Document
            {
                Id = docId,
                CarId = carId,
                WorkerId = workerId
            };
        }
    }
}