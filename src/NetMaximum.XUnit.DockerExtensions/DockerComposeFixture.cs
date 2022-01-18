using Ductus.FluentDocker.Builders;
using Xunit;

namespace NetMaximum.XUnit.DockerExtensions;

public class DockerComposeFixture
{
    public DockerComposeFixture(string files) : this(files, Array.Empty<WaitFor>())
    {
    }

    public DockerComposeFixture(string file, params WaitFor[] waitFors)
    {
        try
        {
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