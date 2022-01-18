using System;
using FluentAssertions;
using Xunit;

namespace NetMaximum.XUnit.DockerExtensions.UnitTests;

public class WaitForTests
{
    [Fact]
    public void Creates_a_new_waitfor()
    {
        // Arrange - Act
        var timeSpan = new TimeSpan(1, 1, 1);
        var subject = new WaitFor("service", "url", timeSpan);
        
        // Assert
        subject.Service.Should().Be("service");
        subject.Url.Should().Be("url");
        subject.Timeout.Should().Be(timeSpan);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Throws_an_argument_exception_for_service_null_or_empty(string value)
    {
        // Arrange
        var timeSpan = new TimeSpan(1, 1, 1);
        var subject = new Action(() =>
        {
            var _ = new WaitFor(value, "url", timeSpan);
        });
        
        // Assert
        subject.Should().Throw<ArgumentException>().WithMessage("service");
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Throws_an_argument_exception_for_url_null_or_empty(string value)
    {
        // Arrange
        var timeSpan = new TimeSpan(1, 1, 1);
        var subject = new Action(() =>
        {
            var _ = new WaitFor("service", value, timeSpan);
        });
        
        // Assert
        subject.Should().Throw<ArgumentException>().WithMessage("url");
    }
}