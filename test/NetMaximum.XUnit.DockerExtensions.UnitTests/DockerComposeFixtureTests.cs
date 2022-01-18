using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace NetMaximum.XUnit.DockerExtensions.UnitTests;

public class DockerComposeFixtureTests
{

    private static readonly string ComposePath = 
        FileUtility.FindFileDirectory(
            Directory.GetCurrentDirectory(), 
            "NetMaximum.XUnit.DockerExtensions.UnitTests.csproj")!;
    
    [Fact]
    public async Task Starts_a_basic_compose()
    {
        // Arrange - Act
        var subject = new Action(() =>
        {
            var _ = new DockerComposeFixture(Path.Combine(ComposePath, "docker-compose.yml"));
        });

        // Assert
        subject.Should().NotThrow();
    }
    
    [Fact]
    public async Task Trying_to_start_docker_with_missing_compose_should_throw_wrapped_exception()
    {
        // Arrange - Act
        var subject = new Action(() =>
        {
            var _ = new DockerComposeFixture(Path.Combine(ComposePath, "docker-missing.yml"));
        });

        // Assert
        subject.Should().Throw<NetMaximumException>();
    }

    [Fact]
    public async Task Starts_a_compose_and_waits_for_service_to_be_available()
    {
        // Arrange
        var _ = new DockerComposeFixture( 
            Path.Combine(ComposePath,"docker-compose.yml"), 
            new WaitFor("aspnet-sample", "http://localhost:8000", TimeSpan.FromSeconds(10)));
        
        // Act
        var client = new HttpClient();
        var request = await client.GetAsync("http://localhost:8000");
        
        // Assert
        Assert.True(request.IsSuccessStatusCode);
    }
}