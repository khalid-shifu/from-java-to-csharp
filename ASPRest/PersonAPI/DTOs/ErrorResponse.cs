namespace PersonAPI.DTOs;

// Standard response DTO for errors (like ErrorResponse in Spring Boot)
public class ErrorResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string? Path { get; set; }
}
