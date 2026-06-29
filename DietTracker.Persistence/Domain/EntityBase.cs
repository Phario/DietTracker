namespace DietTracker.Persistence.Domain;

public class EntityBase
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; set; }
    public UserEntity CreatedBy { get; init; }
    public UserEntity UpdatedBy { get; set; }
}