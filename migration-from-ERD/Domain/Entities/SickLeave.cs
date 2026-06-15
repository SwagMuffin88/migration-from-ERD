using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace migration_from_ERD.Domain.Entities;

public sealed class SickLeave
{
    [Key]
    public int Id { get; set; }

    [ForeignKey(nameof(Employee))]
    public int EmployeeId { get; set; }

    public DateOnly Start { get; set; }
    public DateOnly End { get; set; }

    [Required]
    [MaxLength(100)]
    public required string DocumentNr { get; set; }

    [Required]
    public Employee Employee { get; set; } = null!;
}
