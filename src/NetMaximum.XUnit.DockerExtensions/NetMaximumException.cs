namespace NetMaximum.XUnit.DockerExtensions;

public class NetMaximumException : Exception
{
    public NetMaximumException()
    {
    }

    public NetMaximumException(string message) : base(message)
    {
    }

    public NetMaximumException(string message, Exception innerException) : base(message, innerException)
    {
    }
}