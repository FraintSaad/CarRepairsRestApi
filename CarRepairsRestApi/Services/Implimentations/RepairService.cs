using CarRepairsRestApi.Models;
using CarRepairsRestApi.Repositories.Interfaces;
using CarRepairsRestApi.Services.Interfaces;

namespace CarRepairsRestApi.Services.Implimentations
{
    // Сервис, отвечающий за выполнение работы
    public class RepairService : IRepairService
    {
        private readonly IBaseRepository<Document> _documents;
        private readonly IBaseRepository<Car> _cars;
        private readonly IBaseRepository<Worker> _workers;

        // Инъекция зависимостей через конструктор
        public RepairService(
            IBaseRepository<Document> documents,
            IBaseRepository<Car> cars,
            IBaseRepository<Worker> workers)
        {
            _documents = documents;
            _cars = cars;
            _workers = workers;
        }

        // Метод для выполнения работы
        public void Work()
        {
            var rand = new Random();
            var carId = Guid.NewGuid();
            var workerId = Guid.NewGuid();

            // Создание автомобиля
            var car = new Car
            {
                Id = carId,
                Name = $"Car{rand.Next(1000, 9999)}",
                Number = $"{rand.Next(100000, 999999)}"
            };
            _cars.Create(car);

            // Создание работника
            var worker = new Worker
            {
                Id = workerId,
                Name = $"Worker{rand.Next(1000, 9999)}",
                Position = $"Position{rand.Next(1, 10)}",
                Telephone = $"8916{rand.Next(1000000, 9999999)}"
            };
            _workers.Create(worker);

            // Получение автомобиля и работника по ID
            car = _cars.Get(carId);
            worker = _workers.Get(workerId);

            if (car == null || worker == null)
            {
                // Логика обработки случая, когда автомобиль или работник не найдены
                throw new InvalidOperationException("Car or Worker not found.");
            }

            // Создание документа
            var document = new Document
            {
                CarId = car.Id,
                WorkerId = worker.Id,
                Car = car,
                Worker = worker
            };
            _documents.Create(document);
        }
    }
}
