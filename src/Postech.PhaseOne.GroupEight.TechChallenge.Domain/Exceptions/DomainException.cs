using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions
{
    public  class DomainException : Exception
    {
       
        public DomainException(string message) : base(message) { }

        public static void ThrowWhen(bool invalidRule, string message)
        {
            if (invalidRule)
                throw new DomainException(message);
        }

        
        
    }
}
