namespace ProductManagement.App.Exceptions;

public class NotFoundException(string entityName, object entityId)
    : Exception($"Entity '{entityName}' with ID '{entityId}' was not found.");