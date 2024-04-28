using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Enumerators;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.Common;
using System.Diagnostics.CodeAnalysis;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.ValueObjects
{
    [ExcludeFromCodeCoverage]
    public class InvalidRegionException(string message, RegionNameEnumerator regionName, RegionStateNameEnumerator regionStateName) : DomainException(message)
    {
        public RegionNameEnumerator RegionName { get; } = regionName;
        public RegionStateNameEnumerator RegionStateName { get; } = regionStateName;
    }
}