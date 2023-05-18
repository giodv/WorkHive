using Google.Protobuf.WellKnownTypes;
using WorkHive.Server;
using WotkHive.UnitTest.IntegrationTests.Helpers;
using Xunit.Abstractions;

namespace WotkHive.UnitTest.IntegrationTests;
public class EventServiceTests : IntegrationTestBase
{
    public EventServiceTests(GrpcTestFixture<Startup> fixture, ITestOutputHelper outputHelper)
        : base(fixture, outputHelper)
    {
    }

    #region snippet_SayHelloUnaryTest
    [Fact]
    public async void SayHelloUnaryTest()
    {
        // Arrange
        var client = new WHEvent.WHEventClient(Channel);

        // Act
        var response = await client.CreateEventAsync(new CreateEventRequest { StartDateTime = Timestamp.FromDateTime(DateTime.UtcNow), EndDateTime = Timestamp.FromDateTime(DateTime.UtcNow),Description = "test" });

        // Assert
        Assert.Equal("test", response.Description);
    }
    #endregion

}