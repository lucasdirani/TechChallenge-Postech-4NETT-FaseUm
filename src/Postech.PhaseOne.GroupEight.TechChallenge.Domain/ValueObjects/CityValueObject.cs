namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects
{
    public record CityValueObject
    {
        public RegionValueObject Region { get; init; }
        public string? Name { get; init; }
    }
}