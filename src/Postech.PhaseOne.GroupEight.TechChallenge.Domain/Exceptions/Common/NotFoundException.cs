using System.Diagnostics.CodeAnalysis;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.Common
{
    [ExcludeFromCodeCoverage]
    public class NotFoundException(string message) : Exception(message)
    {
        public static void ThrowWhenNullEntity(object? entity, string errorMessage)
        {
            if (entity is not null) return;
            throw new NotFoundException(errorMessage);
        }

        public static void ThrowWhenNullOrEmptyList(
            IEnumerable<object> list,
            string errorMessage)
        {
            if (list is not null && list.Any()) return;
            throw new NotFoundException(errorMessage);
        }
    }
}