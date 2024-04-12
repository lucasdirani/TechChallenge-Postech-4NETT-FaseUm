using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Interfaces.IEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities
{
    public abstract class EntityBase : IEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        
    }
}
