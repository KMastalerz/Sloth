namespace sloth.Domain.Exceptions;
public class MissingEntryException(string name) : Exception($"Missing {name} entry");
