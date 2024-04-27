using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }

        public static void ThrowWhenNullEntity(object entity, string erroMessage)
        {
            if (entity != null)
                return;

            throw new NotFoundException(erroMessage);
        }

        
        public static void ThrowWhenNullOrEmptyList
                (IEnumerable<object> list, string erroMessage)
        {

            if (list != null && list.Count() > 0)
                return;

            throw new NotFoundException(erroMessage);
        }
    }
}
