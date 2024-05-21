using Postech.PhaseOne.GroupEight.TechChallenge.Domain.Entities;

namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Checkers.Interfaces
{
    public interface IRegisteredContactChecker
    {
        Task<bool> CheckRegisteredContactAsync(ContactEntity contactToBeChecked);
    }
}