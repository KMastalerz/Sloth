namespace Sloth.Domain.Exceptions;
public class InvalidPropertyException(string propertyName) : Exception($"Invalid {propertyName}");
