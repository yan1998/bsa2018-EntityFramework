using System.Collections.Generic;
using System.Threading.Tasks;
using bsa2018_ProjectStructure.DataAccess.Model;

namespace bsa2018_ProjectStructure.DataAccess.Interfaces
{
    public interface  IRepository<TEntity> where TEntity : Entity
    {
        Task<IEnumerable<TEntity>> GetAll();

        Task<TEntity> GetById(int id);

        Task<TEntity> Create(TEntity entity);

        Task<TEntity> Update(int id, TEntity entity);

        Task Delete(int id);
    }
}
