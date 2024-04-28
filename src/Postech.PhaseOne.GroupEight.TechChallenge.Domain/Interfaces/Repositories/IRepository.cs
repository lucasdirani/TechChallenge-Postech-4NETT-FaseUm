using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Interfaces.Entities;
using System.Linq.Expressions;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Interfaces.Repositories
{
    public interface IRepository<T, in TId> 
        where T : IEntity 
        where TId : struct
    {
        Task<bool> ExistsAsync(Expression<Func<T, bool>> expression);
        Task<List<T>> FindAsync(Expression<Func<T, bool>> expression);
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(TId id);
        Task InsertAsync(T entity);
        Task InsertRangeAsync(List<T> entities);
        void SaveChanges();
        Task SaveChangesAsync();
        void Update(T entity);
        void UpdateRange(List<T> entities);
    }
}