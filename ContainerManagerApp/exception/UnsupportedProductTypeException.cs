using System.Diagnostics;
using System.Runtime.Serialization;
namespace ContainerManagerApp;

internal class UnsupportedProductTypeException : Exception
{
    public UnsupportedProductTypeException()
    {
    }

    public UnsupportedProductTypeException(string? message) : base(message)
    {
    }

    public UnsupportedProductTypeException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}