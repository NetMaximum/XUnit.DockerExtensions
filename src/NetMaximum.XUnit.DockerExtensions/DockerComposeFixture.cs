using Ductus.FluentDocker.Builders;
using Xunit;

namespace NetMaximum.XUnit.DockerExtensions;

public class DockerComposeFixture : IDisposable
{
    
    public DockerComposeFixture(string[] files)
    {
        var _ = new Builder()
            .UseContainer()
            .UseCompose()
            .FromFile(files)
            .RemoveOrphans()
           .Build().Start();
    }

    public DockerComposeFixture(string[] files, WaitFor[] waitFors)
    {
        var builder = new Builder()
            .UseContainer()
            .UseCompose()
            .FromFile(files)
            .RemoveOrphans();

        foreach (var waitFor in waitFors)
        {
            builder.WaitForHttp(waitFor.Service, waitFor.Url, (long)waitFor.Timeout.TotalSeconds);
        }
        
        builder.Build().Start();
    }
    
    public void Dispose()
    {
    }
}

[CollectionDefinition("DockerComposeCollection")]
public class DockerComposeCollection : ICollectionFixture<DockerComposeFixture>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}