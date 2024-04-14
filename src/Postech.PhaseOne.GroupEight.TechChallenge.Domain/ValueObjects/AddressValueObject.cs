namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects
{
    public record AddressValueObject
    {
        public CityValueObject? City { get; init; }
        public string? Address { get; init; }
        public string? Number { get; init; }
        public string? Neighborhood { get; init; }
        public string? Complement { get; init; }
        public string? ZipCode { get; init; }
    }
}