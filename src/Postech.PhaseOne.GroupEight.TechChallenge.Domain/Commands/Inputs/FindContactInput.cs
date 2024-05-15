using MediatR;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Outputs;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Inputs
{
    /// <summary>
    /// Object that stores the area code filter value for a contacts search.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class FindContactInput : IRequest<DefaultOutput>
    {
        [MaxLength(2, ErrorMessage = "The area code must contain a maximum of 2 characters.")]
        public string AreaCodeValue { get; set; }
    }
}