using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Interfaces.Entities;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities
{
    public abstract class EntityBase : IEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }       
    }
}