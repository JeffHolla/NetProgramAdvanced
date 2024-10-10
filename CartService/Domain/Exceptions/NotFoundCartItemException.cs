namespace Domain.Exceptions;

public class NotFoundCartItemException(string message) : Exception(message) { }