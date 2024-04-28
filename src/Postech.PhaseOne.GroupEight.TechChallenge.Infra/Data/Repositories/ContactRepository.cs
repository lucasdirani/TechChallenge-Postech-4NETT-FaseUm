using Microsoft.EntityFrameworkCore;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Interfaces.Repositories;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Infra.Data.Repositories
{
    [ExcludeFromCodeCoverage]
    public class ContactRepository : IContactRepository
    {
        public Task<bool> ExistsAsync(Expression<Func<ContactEntity, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<List<ContactEntity>> FindAsync(Expression<Func<ContactEntity, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ContactEntity>> GetAllAsync()
        {
            return await _dbContext.Contacts.ToListAsync();
        }

        public Task<ContactEntity> GetByIdAsync(Guid id)
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