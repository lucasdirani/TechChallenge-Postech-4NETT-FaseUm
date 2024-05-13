using MediatR;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Outputs;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Inputs
{
//    [ExcludeFromCodeCoverage]
    public class ContactInput : IRequest<DefaultOutput>
    {

        public ContactInput()
        {            
        }

        [Required(ErrorMessage ="O nome é obrigatório.")]
        [MaxLength(40, ErrorMessage = "O nome deve conter no máximo 40 caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O sobrenome é obrigatório.")]
        [MaxLength(60, ErrorMessage = "O sobrenome deve conter no máximo 60 caracteres.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [MaxLength(60, ErrorMessage = "O email deve conter no máximo 60 caracteres.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório.")]        
        [MaxLength(9, ErrorMessage = "O email deve conter no máximo 9 caracteres.")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "O código de área é obrigatório.")]
        [MaxLength(2, ErrorMessage = "O email deve conter no máximo 2 caracteres numéricos.")]
        public string AreaCode { get; set; }
    }
}