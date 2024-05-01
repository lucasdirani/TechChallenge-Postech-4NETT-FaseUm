using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Interfaces.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public abstract class EntityBase : IEntity
    {
        public Guid Id { get; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime? ModifiedAt { get; protected set; }     
        public bool Active { get; protected set; }
        
        protected EntityBase()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            ModifiedAt = DateTime.UtcNow;
            Active = true;
        }

        public bool IsActive()
        {
            return Active;
        }

        public void Inactivate()
        {
            Active = false;
            ModifiedAt = DateTime.UtcNow;
        }
    }
}