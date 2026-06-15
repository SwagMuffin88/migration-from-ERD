using System.ComponentModel.DataAnnotations;

namespace migration_from_ERD.Domain.Entities;

public sealed class Company
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public required string Name { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Registry { get; set; }

    [Required]
    [MaxLength(500)]
    public required string Address { get; set; }

    public ICollection<Department> Departments { get; set; } = [];
}
