using System.ComponentModel.DataAnnotations;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects
{
    public record EmailValueObject
    {
        [Required(ErrorMessage = "O email do contato é obrigatório")]
        [StringLength(100)]
        public string? Value { get; init; }
    }
}