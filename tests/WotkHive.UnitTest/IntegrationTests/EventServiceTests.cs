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
    public async void CreateEventRequest()
    {
        // Arrange
        var client = new WHEvent.WHEventClient(Channel);

        // Act
        var response = await client.CreateEventAsync(new CreateEventRequest { StartDateTime = DateTime.UtcNow.Ticks, EndDateTime = DateTime.UtcNow.AddHours(1).Ticks, Description = "test" });

        // Assert
        Assert.Equal("test", response.Description);
    }


    [Fact]
    public async void GetEvents()
    {
        // Arrange
        var client = new WHEvent.WHEventClient(Channel);

        // Act
        await client.CreateEventAsync(new CreateEventRequest { StartDateTime = DateTime.UtcNow.AddHours(3).Ticks, EndDateTime = DateTime.UtcNow.AddHours(5).Ticks, Description = "test" });

        var response = await client.GetEventsAsync(new GetEventFilterRequest { });

        // Assert
        Assert.Equal(2, response.Events.Count());
    }

}