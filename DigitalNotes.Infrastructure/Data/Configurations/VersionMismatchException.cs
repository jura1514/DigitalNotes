namespace DigitalNotes.Infrastructure.Data.Configurations;

public class VersionMismatchException : Exception
{
    public VersionMismatchException()
    {
    }

    public VersionMismatchException(string message)
        : base(message)
    {
    }

    public VersionMismatchException(string message, Exception inner)
        : base(message, inner)
    {
    }
}