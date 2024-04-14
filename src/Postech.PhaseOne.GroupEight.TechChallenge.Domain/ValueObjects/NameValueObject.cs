using System.ComponentModel.DataAnnotations;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.ValueObjects
{
    public record NameValueObject
    {
        [Required(ErrorMessage = "O nome do contato é obrigatório")]
        [StringLength(20)]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "O sobrenome do contato é obrigatório")]
        [StringLength(60)]
        public string? LastName { get; set; }
    }
}