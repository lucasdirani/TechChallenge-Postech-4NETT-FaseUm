using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Interfaces.IRepository
{
    public interface IRepository
    {
        public interface IRepository<T, in TId>
        where T : IEntity
        where TId : struct
        {
            
            Task<bool> ExistsAsync(Expression<Func<T, bool>> expression);
            Task<List<T>> FindAsync(Expression<Func<T, bool>> expression);
            Task<List<T>> GetAllAsync();
            Task<T> GetByIdAsync(TId id);
            Task<T> GetByUniqueIdAsync(Guid uniqueId);
            Task InsertAsync(T entity);
            Task InsertRangeAsync(List<T> entities);
            void LogicalDelete(T obj);
            //Task<List<T>> PaginationAsync(Expression<Func<T, bool>> expression, int skip, int take);
            void SaveChanges();
            Task SaveChangesAsync();
            Task UpdateAsync(T entity);
            void UpdateRange(List<T> entities);
        }
    }
}
