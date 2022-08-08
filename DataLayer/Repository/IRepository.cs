
namespace DataLayer.Repository
{
    public interface IRepository<IEntity> where IEntity : class
    {
        Task<IEnumerable<IEntity>> GetAllAsync();
        Task<IEntity> Add(IEntity entity);
        Task<IEntity> Update(IEntity entity);
        Task<IEntity> Delete(IEntity entity);
        Task<IEntity> FindByIdAsync(int id);
    }
}
