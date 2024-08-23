using CarRepairsRestApi.Models;
using CarRepairsRestApi.Repositories.Interfaces;
using CarRepairsRestApi.Services.Interfaces;

namespace CarRepairsRestApi.Services.Implimentations
{
    // Сервис, отвечающий за выполнение работы
    public class RepairService : IRepairService
    {
        // Инъекция зависимостей для репозиториев
        private IBaseRepository<Document> Documents { get; set; }
        private IBaseRepository<Car> Cars { get; set; }
        private IBaseRepository<Worker> Workers { get; set; }

        // Конструктор, инициализирующий сервисы
        public RepairService(IBaseRepository<Document> documents, IBaseRepository<Car> cars, IBaseRepository<Worker> workers)
        {
            Documents = documents;
            Cars = cars;
            Workers = workers;
        }

        // Метод для выполнения работы
        public void Work()
        {
            // Генерация случайных данных для автомобиля, работника и документа
            var rand = new Random();
            var carId = Guid.NewGuid();
            var workerId = Guid.NewGuid();

            // Создание автомобиля
            Cars.Create(new Car
            {
                Id = carId,
                Name = $"Car{rand.Next()}",
                Number = $"{rand.Next()}"
            });

            // Создание работника
            Workers.Create(new Worker
            {
                Id = workerId,
                Name = $"Worker{rand.Next()}",
                Position = $"Position{rand.Next()}",
                Telephone = $"8916{rand.Next()}{rand.Next()}{rand.Next()}{rand.Next()}{rand.Next()}{rand.Next()}{rand.Next()}"
            });

            // Получение автомобиля и работника по ID
            var car = Cars.Get(carId);
            var worker = Workers.Get(workerId);

            // Создание документа
            Documents.Create(new Document
            {
                CarId = car.Id,
                WorkerId = worker.Id,
                Car = car,
                Worker = worker
            });

        }
    }
}
