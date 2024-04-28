using Microsoft.EntityFrameworkCore;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Interfaces.IRepository;
using System.Linq.Expressions;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Infra.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ContactRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ExistsAsync(Expression<Func<ContactEntity, bool>> expression)
        {
            return await _dbContext.Contacts.AnyAsync(expression);
        }

        public async Task<List<ContactEntity>> FindAsync(Expression<Func<ContactEntity, bool>> expression)
        {
            return await _dbContext.Contacts.Where(expression).ToListAsync();
        }

        public async Task<List<ContactEntity>> GetAllAsync()
        {
            return await _dbContext.Contacts.ToListAsync();
        }

        public async Task<ContactEntity> GetByIdAsync(Guid id)
        {
            return await _dbContext.Contacts.FindAsync(id);
        }

        public async Task<ContactEntity> GetByUniqueIdAsync(Guid id)
        {
            return await _dbContext.Contacts.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task InsertAsync(ContactEntity entity)
        {
            await _dbContext.Contacts.AddAsync(entity);
        }

        public async Task InsertRangeAsync(List<ContactEntity> entities)
        {
            await _dbContext.Contacts.AddRangeAsync(entities);
        }

        public void LogicalDelete(ContactEntity obj)
        {
            obj.IsDeleted = true;
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(ContactEntity entity)
        {
            _dbContext.Contacts.Update(entity);
        }

        public void UpdateRange(List<ContactEntity> entities)
        {
            _dbContext.Contacts.UpdateRange(entities);
        }
    }
}
