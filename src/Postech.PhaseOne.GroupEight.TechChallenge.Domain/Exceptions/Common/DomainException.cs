using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Interfaces.Entities;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Exceptions.Common
{
    [ExcludeFromCodeCoverage]
    public class DomainException(string message) : Exception(message)
    {
        public static void ThrowWhen(bool invalidRule, string message)
        {
            if (invalidRule) throw new DomainException(message);
        }
        public static void ThrowWhenThrereAreErrorMessages(IEnumerable<ValidationResult> results)
        {
            //var messages = string.Empty;
            //foreach (var msg in results)
            //{
            //    messages += $"{(messages.Length>0?" ":"")}{msg.ErrorMessage}";
            //}
            //if (messages.Length>0) throw new DomainException(messages);
            if (results.Count() > 0)
                throw new DomainException(results.ElementAt<ValidationResult>(0).ErrorMessage);
        }
    }
}