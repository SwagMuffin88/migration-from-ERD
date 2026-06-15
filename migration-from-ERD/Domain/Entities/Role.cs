using System.ComponentModel.DataAnnotations;

namespace migration_from_ERD.Domain.Entities;

public sealed class Role
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }

    [Range(0, int.MaxValue)]
    public int AccessLevel { get; set; }

    public ICollection<Employee> Employees { get; set; } = [];
}
