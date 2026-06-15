using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace migration_from_ERD.Domain.Entities;

public sealed class Employee
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public required string FirstName { get; set; }

    [Required]
    [MaxLength(100)]
    public required string LastName { get; set; }

    [Required]
    [MaxLength(50)]
    public required string IdCode { get; set; }

    [Required]
    [EmailAddress]
    [MaxLength(320)]
    public required string Email { get; set; }

    [Required]
    [MaxLength(500)]
    public required string PasswordHash { get; set; }

    [ForeignKey(nameof(Role))]
    public int RoleId { get; set; }

    [ForeignKey(nameof(Department))]
    public int DepartmentId { get; set; }

    public DateOnly EmployedSince { get; set; }

    [Required]
    public Role Role { get; set; } = null!;

    [Required]
    public Department Department { get; set; } = null!;

    public ICollection<Vacation> Vacations { get; set; } = [];
    public ICollection<SickLeave> SickLeaves { get; set; } = [];
    public ICollection<HealthCheck> HealthChecks { get; set; } = [];
}
