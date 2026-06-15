namespace migration_from_ERD.Infrastructure.Configuration;

public sealed class DatabaseSettings
{
    public const string SectionName = "Database";

    public required string Host { get; init; }
    public int Port { get; init; } = 1433;
    public required string Name { get; init; }
    public bool Encrypt { get; init; } = true;
    public bool TrustServerCertificate { get; init; }
}
