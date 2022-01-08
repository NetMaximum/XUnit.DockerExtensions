using Ductus.FluentDocker.Builders;
using Xunit;

namespace NetMaximum.XUnit.DockerExtensions;

public class DockerComposeFixture : IDisposable
{
    
    public DockerComposeFixture(string[] files)
    {
   // var file = Path.Combine(FileUtility.FindFileDirectory(Directory.GetCurrentDirectory(), "RotaAPI.sln")!,"docker-compose.yml");
        var _ = new Builder()
            .UseContainer()
            .UseCompose()
            .FromFile(files)
            .RemoveOrphans()
            .WaitForHttp("eventstore", "http://localhost:2113", timeout: 60000)
            .Build().Start();
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