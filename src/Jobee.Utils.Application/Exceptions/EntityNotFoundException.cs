namespace Jobee.Utils.Application.Exceptions;

public class EntityNotFoundException : Exception
{
    public string EntityType { get; }
    
    public string EntityId { get; }
    
    public EntityNotFoundException(string entityType, object entityId) 
        : base($"Entity '{entityType}' with ID '{entityId}' was not found.")
    {
        EntityType = entityType;
        EntityId = entityId.ToString()!;
    }
}