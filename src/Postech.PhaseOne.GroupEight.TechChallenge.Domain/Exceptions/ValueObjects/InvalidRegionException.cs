using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Enumerators;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.ValueObjects
{
    public class InvalidRegionException(string message, RegionNameEnumerator regionName, RegionStateNameEnumerator regionStateName) : Exception(message)
    {
        public RegionNameEnumerator RegionName { get; } = regionName;
        public RegionStateNameEnumerator RegionStateName { get; } = regionStateName;
    }
}