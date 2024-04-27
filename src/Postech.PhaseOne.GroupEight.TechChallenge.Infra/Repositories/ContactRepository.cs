using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Interfaces.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Infra.Repositories
{
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

        public Task<List<ContactEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ContactEntity> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ContactEntity> GetByUniqueIdAsync(Guid uniqueId)
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync(ContactEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task InsertRangeAsync(List<ContactEntity> entities)
        {
            throw new NotImplementedException();
        }

        public void LogicalDelete(ContactEntity obj)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ContactEntity entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateRange(List<ContactEntity> entities)
        {
            throw new NotImplementedException();
        }
    }
}
