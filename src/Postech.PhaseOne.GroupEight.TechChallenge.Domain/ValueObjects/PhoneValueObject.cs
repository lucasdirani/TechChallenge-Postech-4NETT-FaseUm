using System.ComponentModel.DataAnnotations;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects
{
    public record PhoneValueObject
    {
        public AreaCodeValueObject? AreaCode { get; init; }

        [Required(ErrorMessage = "O número de telefone é obrigatório")]
        [StringLength(11)]
        public string? Number { get; init; }
        public bool IsMainPhone { get; init; }
    }
}