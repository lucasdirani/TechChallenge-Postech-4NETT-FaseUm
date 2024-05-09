using MediatR;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Outputs;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Inputs
{
    public class FindContactInput : IRequest<DefaultOutput>
    {
        public string AreaCodeValue { get; set; }
    }
}