namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Interfaces.Entities
{
    public interface IEntity
    {
        Guid Id { get; }
        DateTime CreatedAt { get; }
        DateTime? ModifiedAt { get; }
    }
}