namespace sloth.Domain.Exceptions;
public class LockedPasswordException(DateTime lockExpirationDate) : Exception($"Password is locked until: {lockExpirationDate}");
