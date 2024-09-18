namespace Sloth.Domain.Exceptions;
public class MissingAccessException(string resourceName, string resourceKey) : Exception($"Requested locked access to resource: {resourceName} with ID {resourceKey}");
