using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public interface IRepository<IEntity> where IEntity : class
    {
        IEnumerable<IEntity> All { get; }
        void Add(IEntity entity);
        void Update(IEntity entity);
        void Delete(IEntity entity);
        IEntity FindById(int id);
        void Save();
    }
}
