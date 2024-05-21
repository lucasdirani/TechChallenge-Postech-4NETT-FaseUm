using Microsoft.EntityFrameworkCore;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Interfaces.Repositories;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects;
using Postech.PhaseOne.GroupEight.TechChallenge.Infra.Data.Contexts;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Infra.Data.Repositories
{
    [ExcludeFromCodeCoverage]
    public class ContactRepository(ContactManagementDbContext dbContext) : IContactRepository
    {
        private readonly ContactManagementDbContext _dbContext = dbContext;

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

        public async Task<AreaCodeValueObject?> GetAreaCodeByValueAsync(string areaCodeValue)
        {
            return await _dbContext.AreaCodes.FirstOrDefaultAsync(areaCode => areaCode.Value == areaCodeValue);
        }

        public async Task<ContactEntity?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Contacts.FindAsync(id);
        }

        public async Task<ContactPhoneValueObject?> GetContactPhoneByNumberAndAreaCodeValueAsync(string phoneNumber, string areaCodeValue)
        {
            return await _dbContext.ContactPhones.FirstOrDefaultAsync(contactPhone => contactPhone.Number == phoneNumber && contactPhone.AreaCode.Value == areaCodeValue);
        }

        public async Task<IEnumerable<ContactEntity>> GetContactsByAreaCodeValueAsync(string areaCodeValue)
        {
            return await _dbContext.Contacts.Where(contact => contact.ContactPhone.AreaCode.Value == areaCodeValue && contact.Active).ToListAsync();
        }

        public async Task<IEnumerable<ContactEntity>> GetContactsByContactPhoneAsync(ContactPhoneValueObject contactPhone)
        {
            return await _dbContext
                .Contacts
                .Where(contact => contact.ContactPhone.Number == contactPhone.Number && contact.ContactPhone.AreaCode.Value == contactPhone.AreaCode.Value)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task InsertAsync(ContactEntity entity)
        {
            await _dbContext.Contacts.AddAsync(entity);
        }

        public async Task InsertRangeAsync(List<ContactEntity> entities)
        {
            await _dbContext.Contacts.AddRangeAsync(entities);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Update(ContactEntity entity)
        {
            _dbContext.Contacts.Update(entity);
        }

        public void UpdateRange(List<ContactEntity> entities)
        {
            _dbContext.Contacts.UpdateRange(entities);
        }
    }
}