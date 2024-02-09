using System.Linq;
using System.Threading.Tasks;

namespace DBALayer.Interfaces
{
    public interface IBaseRepo<T>
    {
        Task Create(T entity);

        IQueryable<T> GetAll();

        Task Delete(T entity);

        Task<T> Update(T entity);
    }
}
