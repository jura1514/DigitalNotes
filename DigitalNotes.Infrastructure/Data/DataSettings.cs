namespace DigitalNotes.Infrastructure.Data;

internal class DataSettings
{
    public const string Section = "Data";
    public required string ConnectionString { get; init; }
    public bool UseInMemory { get; init; }
}