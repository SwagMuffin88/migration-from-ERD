using Microsoft.EntityFrameworkCore;
using migration_from_ERD.Domain.Entities;

namespace migration_from_ERD.Data;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options)
{
    public DbSet<Company> Companies => Set<Company>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Vacation> Vacations => Set<Vacation>();
    public DbSet<SickLeave> SickLeaves => Set<SickLeave>();
    public DbSet<HealthCheck> HealthChecks => Set<HealthCheck>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigureCompany(modelBuilder);
        ConfigureDepartment(modelBuilder);
        ConfigureRole(modelBuilder);
        ConfigureEmployee(modelBuilder);
        ConfigureVacation(modelBuilder);
        ConfigureSickLeave(modelBuilder);
        ConfigureHealthCheck(modelBuilder);
    }

    private static void ConfigureCompany(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<Company>();

        entity.HasIndex(company => company.Registry).IsUnique();
    }

    private static void ConfigureDepartment(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<Department>();

        entity.HasIndex(department => new { department.CompanyId, department.Name }).IsUnique();
        entity.HasOne(department => department.Company)
            .WithMany(company => company.Departments)
            .HasForeignKey(department => department.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    private static void ConfigureRole(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<Role>();

        entity.HasIndex(role => role.Name).IsUnique();
        entity.ToTable(table => table.HasCheckConstraint(
            "CK_Roles_AccessLevel_NonNegative",
            "[AccessLevel] >= 0"));
    }

    private static void ConfigureEmployee(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<Employee>();

        entity.HasIndex(employee => employee.IdCode).IsUnique();
        entity.HasIndex(employee => employee.Email).IsUnique();
        entity.HasOne(employee => employee.Role)
            .WithMany(role => role.Employees)
            .HasForeignKey(employee => employee.RoleId)
            .OnDelete(DeleteBehavior.Restrict);
        entity.HasOne(employee => employee.Department)
            .WithMany(department => department.Employees)
            .HasForeignKey(employee => employee.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    private static void ConfigureVacation(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<Vacation>();

        entity.HasOne(vacation => vacation.Employee)
            .WithMany(employee => employee.Vacations)
            .HasForeignKey(vacation => vacation.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);
        entity.ToTable(table => table.HasCheckConstraint(
            "CK_Vacations_DateRange",
            "[End] >= [Start]"));
    }

    private static void ConfigureSickLeave(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<SickLeave>();

        entity.HasIndex(sickLeave => sickLeave.DocumentNr).IsUnique();
        entity.HasOne(sickLeave => sickLeave.Employee)
            .WithMany(employee => employee.SickLeaves)
            .HasForeignKey(sickLeave => sickLeave.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);
        entity.ToTable(table => table.HasCheckConstraint(
            "CK_SickLeaves_DateRange",
            "[End] >= [Start]"));
    }

    private static void ConfigureHealthCheck(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<HealthCheck>();

        entity.HasOne(healthCheck => healthCheck.Employee)
            .WithMany(employee => employee.HealthChecks)
            .HasForeignKey(healthCheck => healthCheck.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);
        entity.ToTable(table => table.HasCheckConstraint(
            "CK_HealthChecks_DateRange",
            "[NextCheck] >= [CheckedOn]"));
    }
}
