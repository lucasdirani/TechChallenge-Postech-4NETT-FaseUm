using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Commands.Outputs
{
    public class ContactListOutput
    {
        public ContactListOutput(bool success, List<ContactEntity> list)
        {
            Success = success;
            List = list;
        }

        public ContactListOutput(bool success, string message)
        {
            Success = success;
            Message = message;

        }
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<ContactEntity> List { get; set; }
    }
}