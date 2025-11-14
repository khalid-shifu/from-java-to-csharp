using System.ComponentModel.DataAnnotations;

namespace PersonAPI.Models;

// Like @Entity in Spring Boot
public class Person
{
    // Like @Id @GeneratedValue(strategy = GenerationType.IDENTITY)
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Range(0, 150)]
    public int Age { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; } = string.Empty;

    [Phone]
    [StringLength(20)]
    public string? Phone { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
