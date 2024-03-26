using System.Runtime.Serialization;

namespace ContainerManagerApp;

internal class HazardException : Exception
{
    public HazardException()
    {
    }

    public HazardException(string? message) : base(message)
    {
    }

    public HazardException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}