using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.Common
{
    [ExcludeFromCodeCoverage]
    public class EntityInactiveException(string message) : DomainException(message)
    {
        public static void ThrowWhenIsInactive(EntityBase entity, string errorMessage)
        {
            if (!entity.IsActive())
            {
                throw new EntityInactiveException(errorMessage);
            }
        }
    }
}