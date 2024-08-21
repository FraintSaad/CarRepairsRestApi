using CarRepairsRestApi.Database;
using CarRepairsRestApi.Models.Base;
using CarRepairsRestApi.Repositories.Interfaces;

namespace CarRepairsRestApi.Repositories.Implimentations
{
    // Базовый репозиторий для моделей, наследующих BaseModel
    public class BaseRepository<TDbModel> : IBaseRepository<TDbModel> where TDbModel : BaseModel
    {
        private ApplicationContext Context { get; set; }

        public BaseRepository(ApplicationContext context)
        {
            Context = context;
        }

        // Создание модели
        public TDbModel Create(TDbModel model)
        {
            Context.Set<TDbModel>().Add(model);
            Context.SaveChanges();
            return model;
        }

        // Удаление модели по ID
        public void Delete(Guid id)
        {
            var toDelete = Context.Set<TDbModel>().FirstOrDefault(m => m.Id == id);
            Context.Set<TDbModel>().Remove(toDelete);
            Context.SaveChanges();
        }

        // Получение всех моделей
        public List<TDbModel> GetAll()
        {
            return Context.Set<TDbModel>().ToList();
        }

        // Обновление модели
        public TDbModel Update(TDbModel model)
        {
            var toUpdate = Context.Set<TDbModel>().FirstOrDefault(m => m.Id == model.Id);
            if (toUpdate != null)
            {
                toUpdate = model;
            }
            Context.Update(toUpdate);
            Context.SaveChanges();
            return toUpdate;
        }

        // Получение модели по ID
        public TDbModel Get(Guid id)
        {
            return Context.Set<TDbModel>().FirstOrDefault(m => m.Id == id);
        }
    }
}
