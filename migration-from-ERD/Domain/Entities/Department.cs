using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace migration_from_ERD.Domain.Entities;

public sealed class Department
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public required string Name { get; set; }

    [ForeignKey(nameof(Company))]
    public int CompanyId { get; set; }

    [Required]
    public Company Company { get; set; } = null!;

    public ICollection<Employee> Employees { get; set; } = [];
}
