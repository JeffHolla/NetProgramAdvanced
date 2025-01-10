namespace CartService.Common.Exceptions;

public class NotFoundCartItemException(string message) : Exception(message) { }