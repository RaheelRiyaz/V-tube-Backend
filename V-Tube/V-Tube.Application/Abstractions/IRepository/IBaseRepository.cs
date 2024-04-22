using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace V_Tube.Application.Abstractions.IServices
{
    public interface IBaseRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> FindOneAsync(Guid id);
        Task<T?> FindOneAsync(Expression<Func<T,bool>> expression);
        Task<T?> FirstOrDefaultAsync(Expression<Func<T,bool>> expression);
        Task<IQueryable<T>> FilterAsync(Expression<Func<T,bool>> expression);
        Task<bool> ExistsAsync(Expression<Func<T,bool>> expression);
        Task<int> InsertAsync(T model);
        Task<int> InsertRangeAsync(List<T> models);
        Task<int> UpdateAsync(T model); 
        Task<int> DeleteAsync(Guid id); 
        Task<int> DeleteRangeAsync(List<Guid> ids); 
        Task<int> DeleteRangeAsync(List<T> models); 
        Task<int> DeleteAsync(T model); 
    }
}
