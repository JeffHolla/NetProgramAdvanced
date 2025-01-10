namespace CartService.Common.Exceptions;

public class ValidationFailedException(string message) : Exception(message) {}
