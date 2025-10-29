namespace ProvaDev.Domain.Entities.Common;

public abstract class Entity
{ 
    public int Id { get; protected set; }
    public DateTime CreatedAt { get; protected set; }
    public DateTime? UpdatedAt { get; protected set; }

    public void TouchUpdated() => UpdatedAt = DateTime.UtcNow;
}