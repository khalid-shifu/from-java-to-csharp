using System.ComponentModel.DataAnnotations;

namespace PersonAPI.Models;

public class Student
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string Major { get; set; } = string.Empty;

    [Range(1, 10)]
    public int Semester { get; set; }
}
