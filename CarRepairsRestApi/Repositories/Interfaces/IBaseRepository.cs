using CarRepairsRestApi.Models.Base;

namespace CarRepairsRestApi.Repositories.Interfaces
{
    //интерфейс для базового репозитория
    public interface IBaseRepository<TDbModel> where TDbModel : BaseModel
    {
        public List<TDbModel> GetAll();
        public TDbModel Get(Guid id);
        public TDbModel Create(TDbModel model);
        public TDbModel Update(TDbModel model);
        public void Delete(Guid id);
    }
}
