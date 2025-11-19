namespace PersonAPI.Exceptions;

// Custom exceptions (like Spring Boot custom exceptions)
public class ResourceNotFoundException : Exception
{
    public ResourceNotFoundException(string message) : base(message) { }
}

public class DuplicateResourceException : Exception
{
    public DuplicateResourceException(string message) : base(message) { }
}

public class ValidationException : Exception
{
    public ValidationException(string message) : base(message) { }
}
