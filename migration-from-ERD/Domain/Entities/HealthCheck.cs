using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace migration_from_ERD.Domain.Entities;

public sealed class HealthCheck
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(Employee))]
    public int EmployeeId { get; set; }

    public DateOnly CheckedOn { get; set; }
    public DateOnly NextCheck { get; set; }

    [Required]
    [MaxLength(500)]
    public required string Result { get; set; }

    [Required]
    public Employee Employee { get; set; } = null!;
}
