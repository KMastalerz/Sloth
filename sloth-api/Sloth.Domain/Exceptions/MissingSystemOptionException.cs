namespace Sloth.Domain.Exceptions;
public class MissingSystemOptionException(string optionkey) : Exception($"System Option: {optionkey} is not provided, please check SystemOption table");
