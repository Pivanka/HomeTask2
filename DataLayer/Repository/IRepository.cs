
namespace DataLayer.Repository
{
    public interface IRepository<IEntity> where IEntity : class
    {
        Task<IEnumerable<IEntity>> GetAllAsync();
        void Add(IEntity entity);
        void Update(IEntity entity);
        void Delete(IEntity entity);
        Task<IEntity> FindByIdAsync(int id);
        void Save();
    }
}
