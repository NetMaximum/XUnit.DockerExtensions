namespace NetMaximum.XUnit.DockerExtensions;

public sealed class WaitFor
{
    public string Service { get; }
    public string Url { get; }
    public TimeSpan Timeout { get; }

    public WaitFor(string service, string url, TimeSpan timeout)
    {
        if (string.IsNullOrEmpty(service) || string.IsNullOrWhiteSpace(service))
        {
            throw new ArgumentException(nameof(service));
        }
        
        if (string.IsNullOrEmpty(url) || string.IsNullOrWhiteSpace(url))
        {
            throw new ArgumentException(nameof(url));
        }

        Service = service;
        Url = url;
        Timeout = timeout;
    }
}