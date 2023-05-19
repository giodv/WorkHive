using Google.Protobuf.WellKnownTypes;
using WotkHive.Tests.IntegrationTests.Helpers;
using WotkHive.UnitTest.IntegrationTests;
using WotkHive.UnitTest.IntegrationTests.Helpers;
using Xunit.Abstractions;

namespace WotkHive.Tests.IntegrationTests;
public class EventServiceTests : IntegrationTestBase
{
    public EventServiceTests(GrpcTestFixture<Program> fixture, ITestOutputHelper outputHelper)
        : base(fixture, outputHelper)
    {
    }

    [Fact]
    public async void SayHelloUnaryTest()
    {
        // Arrange
        var client = new WHEvent.WHEventClient(Channel);

        // Act
        var response = await client.CreateEventAsync(new CreateEventRequest { StartDateTime = Timestamp.FromDateTime(DateTime.UtcNow), EndDateTime = Timestamp.FromDateTime(DateTime.UtcNow), Description = "test" });

        // Assert
        Assert.Equal("test", response.Description);
    }

}