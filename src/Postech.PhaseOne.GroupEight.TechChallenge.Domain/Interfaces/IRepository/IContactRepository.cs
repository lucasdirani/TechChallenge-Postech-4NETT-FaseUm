using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Postech.PhaseOne.GroupEight.TechChallenge.Domain.Interfaces.IRepository.IRepository;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Interfaces.IRepository
{
    public interface IContactRepository : IRepository<ContactEntity, int>
    {
    }
}
