using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using V_Tube.Application.Abstractions.IServices;
using V_Tube.Domain.Models;
using V_Tube.Persisitence.DataBase;

namespace V_Tube.Persisitence.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity, new()
    {
        protected readonly VTubeDbContext dbContext;
        private readonly DbSet<T> db_set;

        public BaseRepository(VTubeDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.db_set = dbContext.Set<T>();
        }


        public async Task<int> DeleteAsync(Guid id)
        {
            db_set.Remove(new T { Id = id });
            return await SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(T model)
        {
            db_set.Remove(model);
            return await SaveChangesAsync();
        }

        public async Task<int> DeleteRangeAsync(List<Guid> ids)
        {
            var models = new List<T>();

            foreach (var id in ids)
            {
                models.Add(new T { Id = id });
            }

            db_set.RemoveRange(models);

            return await SaveChangesAsync();
        }

        public async Task<int> DeleteRangeAsync(List<T> models)
        {
            db_set.RemoveRange(models);
            return await SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> expression)
        {
            return await db_set.AnyAsync(expression);
        }

        public async Task<IQueryable<T>> FilterAsync(Expression<Func<T, bool>> expression)
        {
            return await Task.Run(() =>
             {
                 return db_set.Where(expression);
             });
        }

        public async Task<T?> FIndOneAsync(Guid id)
        {
            return await db_set.FindAsync(id);
        }

        public async Task<T?> FindOneAsync(Expression<Func<T, bool>> expression)
        {
            return await db_set.FindAsync(expression);
        }

        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            return await db_set.FirstOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Task.FromResult(db_set);
        }

        public async Task<int> InsertAsync(T model)
        {
            await db_set.AddAsync(model);
            return await SaveChangesAsync();
        }

        public async Task<int> InsertRangeAsync(List<T> models)
        {
            await db_set.AddRangeAsync(models);

            return await SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(T model)
        {
            db_set.Update(model);
            return await SaveChangesAsync();
        }



        private async Task<int> SaveChangesAsync() =>
             await dbContext.SaveChangesAsync();
    }
}
