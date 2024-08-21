using CarRepairsRestApi.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace CarRepairsRestApi.Database
{
    // Класс ApplicationContext наследует от DbContext и настраивает подключение к базе данных.
    public class ApplicationContext : DbContext
    {
        // Определяем DbSet для каждой модели, чтобы Entity Framework мог работать с ними.
        public DbSet<Car> Cars { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Worker> Workers { get; set; }

        // Конструктор, который принимает параметры подключения к базе данных
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
