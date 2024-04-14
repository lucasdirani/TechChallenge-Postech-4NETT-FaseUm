namespace Postech.PhaseOne.GroupEight.TechChallenge.Domain.Interfaces.Entities
{
    public interface IEntity
    {
        Guid Id { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime? ModifiedAt { get; set; }
    }
}