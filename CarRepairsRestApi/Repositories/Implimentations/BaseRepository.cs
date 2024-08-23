using CarRepairsRestApi.Database;
using CarRepairsRestApi.Models.Base;
using CarRepairsRestApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarRepairsRestApi.Repositories.Implimentations
{
    // Базовый репозиторий для моделей, наследующих BaseModel
    public class BaseRepository<TDbModel> : IBaseRepository<TDbModel> where TDbModel : BaseModel
    {
        private readonly ApplicationContext _context;

        public BaseRepository(ApplicationContext context)
        {
            _context = context;
        }

        // Создание модели
        public TDbModel Create(TDbModel model)
        {
            _context.Set<TDbModel>().Add(model);
            _context.SaveChanges();
            return model;
        }

        // Удаление модели по ID
        public void Delete(Guid id)
        {
            var toDelete = _context.Set<TDbModel>().FirstOrDefault(m => m.Id == id);
            if (toDelete != null)
            {
                _context.Set<TDbModel>().Remove(toDelete);
                _context.SaveChanges();
            }
            else
            {
                // Логика обработки ситуации, когда объект не найден
                throw new KeyNotFoundException($"Object with ID {id} not found.");
            }
        }

        // Получение всех моделей
        public List<TDbModel> GetAll()
        {
            return _context.Set<TDbModel>().ToList();
        }

        // Обновление модели
        public TDbModel Update(TDbModel model)
        {
            var toUpdate = _context.Set<TDbModel>().FirstOrDefault(m => m.Id == model.Id);
            if (toUpdate != null)
            {
                _context.Entry(toUpdate).CurrentValues.SetValues(model);
                _context.SaveChanges();
            }
            else
            {
                throw new KeyNotFoundException($"Object with ID {model.Id} not found.");
            }
            return toUpdate;
        }

        // Получение модели по ID
        public TDbModel Get(Guid id)
        {
            return _context.Set<TDbModel>().FirstOrDefault(m => m.Id == id);
        }
    }
}
