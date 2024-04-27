using MediatR;
using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Outputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Inputs
{
    public class ContactInput: IRequest<DefaultOutput>
    {
        public string Name { get; set; }
    }
}
