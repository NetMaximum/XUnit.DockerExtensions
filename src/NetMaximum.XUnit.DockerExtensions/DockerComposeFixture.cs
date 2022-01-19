using Ductus.FluentDocker.Builders;
using Xunit;

namespace NetMaximum.XUnit.DockerExtensions;

public class DockerComposeFixture
{
    public bool IsInited { get; private set; } = false;

    private readonly object _lockObject = new Object();

    public void Init(string files)
    {
        Init(files, Array.Empty<WaitFor>());
    }

    public void Init(string file, params WaitFor[] waitFors)
    {
        try
        {
            if (IsInited)
            {
                return;
            }

            lock (_lockObject)
            {
                if (IsInited)
                {
                    return;
                }
                
                var builder = new Builder()
                    .UseContainer()
                    .UseCompose()
                    .FromFile(file)
                    .RemoveOrphans();

                foreach (var waitFor in waitFors)
                {
                    builder.WaitForHttp(waitFor.Service, waitFor.Url, (long) waitFor.Timeout.TotalSeconds);
                }

                builder.Build().Start();
                IsInited = true;
            }
        }
        catch (Exception e)
        {
            throw new NetMaximumException(e.Message);
        }
    }
}

[CollectionDefinition("DockerComposeCollection")]
public class DockerComposeCollection : ICollectionFixture<DockerComposeFixture>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}