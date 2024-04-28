using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities;
using static Postech.PhaseOne.GroupEight.TechChallenge.Domain.Interfaces.IRepository.IRepository;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Interfaces.IRepository
{
    public interface IContactRepository : IRepository<ContactEntity, Guid>
    {
    }
}
