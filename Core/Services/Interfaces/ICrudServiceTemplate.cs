using Database.Commands;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services.Interfaces
{
    public interface ICrudServiceTemplate<T>
    {
        Task<DbStatus> Add(T entity);
        Task<DbStatus> Delete(T entity);
        Task<DbStatus> Update(T entity);

        Task<T> GetByPrimaryKey(T entity);
        Task<IList<T>> GetAll();

        Task<IList<T>> GetRange(int begin, int count);
        Task<T> GetByUniqueIdentifiers(string[] propertyNames, T entity);

        Task<int> GetTotalNumberOfItems();
    }
}
