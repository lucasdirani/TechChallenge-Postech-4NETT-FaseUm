using System.Diagnostics.CodeAnalysis;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.ViewModels
{
    [ExcludeFromCodeCoverage]
    public record FindContactByAreaCodeViewModel
    {
        public string ContactFirstName { get; init; }
        public string ContactLastName { get; init; }
        public string ContactEmail { get; init; }
        public string ContactPhoneNumber { get; init; }
        public string ContactPhoneAreaCode { get; init; }
    }
}