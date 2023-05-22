using FluentAssertions;
using WorkHive.Domain;
using WotkHive.Tests.IntegrationTests.Helpers;
using WotkHive.UnitTest.IntegrationTests;
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
    public async void GetEvents1()
    {
        // Arrange
        var client = new WHEvent.WHEventClient(Channel);

        // Act
        await client.CreateEventAsync(new CreateEventRequest { StartDateTime = DateTime.UtcNow.AddHours(3).Ticks, EndDateTime = DateTime.UtcNow.AddHours(5).Ticks, Description = "test" });

        var response = await client.GetEventsAsync(new GetEventFilterRequest { });

        // Assert
        Assert.Equal(2, response.Events.Count);
    }

    [Fact]
    public async void GetEvents2()
    {
        // Arrange
        var client = new WHEvent.WHEventClient(Channel);

        // Act

        await client.CreateEventAsync(new CreateEventRequest { StartDateTime = DateTime.UtcNow.AddHours(-1).Ticks, EndDateTime = DateTime.UtcNow.AddHours(1).Ticks, Description = "test" });
        await client.CreateEventAsync(new CreateEventRequest { StartDateTime = DateTime.UtcNow.AddHours(3).Ticks, EndDateTime = DateTime.UtcNow.AddHours(5).Ticks, Description = "test" });

        var response = await client.GetEventsAsync(new GetEventFilterRequest { });

        // Assert
        Assert.Equal(3, response.Events.Count);
    }

    [Fact]
    public async void GetEventsWithFilter1()
    {
        // Arrange
        var client = new WHEvent.WHEventClient(Channel);

        // Act

        await client.CreateEventAsync(new CreateEventRequest { StartDateTime = DateTime.UtcNow.Ticks, EndDateTime = DateTime.UtcNow.AddHours(1).Ticks, Description = "test" });
        await client.CreateEventAsync(new CreateEventRequest { StartDateTime = DateTime.UtcNow.AddHours(3).Ticks, EndDateTime = DateTime.UtcNow.AddHours(5).Ticks, Description = "test" });

        var response = await client.GetEventsAsync(new GetEventFilterRequest { StartDateTime = DateTime.UtcNow.AddHours(-5).Ticks });

        // Assert
        Assert.Equal(3, response.Events.Count);
    }

    [Fact]
    public async void GetEventsWithFilter2()
    {
        // Arrange
        var client = new WHEvent.WHEventClient(Channel);

        // Act

        await client.CreateEventAsync(new CreateEventRequest { StartDateTime = DateTime.UtcNow.AddHours(-1).Ticks, EndDateTime = DateTime.UtcNow.AddHours(1).Ticks, Description = "test" });
        await client.CreateEventAsync(new CreateEventRequest { StartDateTime = DateTime.UtcNow.AddHours(3).Ticks, EndDateTime = DateTime.UtcNow.AddHours(5).Ticks, Description = "test" });

        var response = await client.GetEventsAsync(new GetEventFilterRequest { StartDateTime = DateTime.UtcNow.Ticks });

        // Assert
        Assert.Equal(2, response.Events.Count);
    }

    [Fact]
    public async void GetEventsWithFilter3()
    {
        // Arrange
        var client = new WHEvent.WHEventClient(Channel);

        // Act

        await client.CreateEventAsync(new CreateEventRequest { StartDateTime = DateTime.UtcNow.AddHours(-1).Ticks, EndDateTime = DateTime.UtcNow.AddHours(1).Ticks, Description = "test" });
        await client.CreateEventAsync(new CreateEventRequest { StartDateTime = DateTime.UtcNow.AddHours(3).Ticks, EndDateTime = DateTime.UtcNow.AddHours(5).Ticks, Description = "test" });

        var response = await client.GetEventsAsync(new GetEventFilterRequest { StartDateTime = DateTime.UtcNow.Ticks, EndDateTime = DateTime.UtcNow.AddHours(3).Ticks });

        // Assert
        Assert.Single(response.Events);
    }

    [Fact]
    public async void GetEventsWithFilter4()
    {
        // Arrange
        var client = new WHEvent.WHEventClient(Channel);

        // Act

        await client.CreateEventAsync(new CreateEventRequest { StartDateTime = DateTime.UtcNow.AddHours(-1).Ticks, EndDateTime = DateTime.UtcNow.AddHours(1).Ticks, Description = "test" });
        await client.CreateEventAsync(new CreateEventRequest { StartDateTime = DateTime.UtcNow.AddHours(3).Ticks, EndDateTime = DateTime.UtcNow.AddHours(5).Ticks, Description = "test" });

        var response = await client.GetEventsAsync(new GetEventFilterRequest { Location = "Nap" });

        // Assert
        Assert.Single(response.Events);
    }

    [Fact]
    public async void GetEventsWithFilter5()
    {
        // Arrange
        var client = new WHEvent.WHEventClient(Channel);

        // Act

        await client.CreateEventAsync(new CreateEventRequest { StartDateTime = DateTime.UtcNow.AddHours(-1).Ticks, EndDateTime = DateTime.UtcNow.AddHours(1).Ticks, Description = "test", Location = "Milan" });
        await client.CreateEventAsync(new CreateEventRequest { StartDateTime = DateTime.UtcNow.AddHours(3).Ticks, EndDateTime = DateTime.UtcNow.AddHours(5).Ticks, Description = "test", Location = "Milan" });

        var response = await client.GetEventsAsync(new GetEventFilterRequest { Location = "Mil" });

        // Assert
        response.Events.Should().HaveCount(2);
    }

    [Fact]
    public async void GetEventsWithFilter6()
    {
        // Arrange
        var client = new WHEvent.WHEventClient(Channel);

        // Act

        await client.CreateEventAsync(new CreateEventRequest { StartDateTime = DateTime.UtcNow.AddHours(-1).Ticks, EndDateTime = DateTime.UtcNow.AddHours(1).Ticks, Description = "test", Location = "Milan" });
        await client.CreateEventAsync(new CreateEventRequest { StartDateTime = DateTime.UtcNow.AddHours(3).Ticks, EndDateTime = DateTime.UtcNow.AddHours(5).Ticks, Description = "test", Location = "Milan" });

        var response = await client.GetEventsAsync(new GetEventFilterRequest { OrganizerId = "B4B117AA-3AD8-4D13-802A-B8BAD0DC8E95" });

        // Assert
        response.Events.Should().HaveCount(3);
    }

    [Fact]
    public async void GetEventsWithFilter7()
    {
        // Arrange
        var client = new WHEvent.WHEventClient(Channel);

        // Act

        await client.CreateEventAsync(new CreateEventRequest { StartDateTime = DateTime.UtcNow.AddHours(-1).Ticks, EndDateTime = DateTime.UtcNow.AddHours(1).Ticks, Description = "test", Location = "Milan", EventType = (int)WHEventType.Fun });
        await client.CreateEventAsync(new CreateEventRequest { StartDateTime = DateTime.UtcNow.AddHours(3).Ticks, EndDateTime = DateTime.UtcNow.AddHours(5).Ticks, Description = "test", Location = "Milan", EventType = (int)WHEventType.Work });
        await client.CreateEventAsync(new CreateEventRequest { StartDateTime = DateTime.UtcNow.AddHours(3).Ticks, EndDateTime = DateTime.UtcNow.AddHours(5).Ticks, Description = "test", Location = "Milan", EventType = (int)WHEventType.WorkAndFun });
        await client.CreateEventAsync(new CreateEventRequest { StartDateTime = DateTime.UtcNow.AddHours(3).Ticks, EndDateTime = DateTime.UtcNow.AddHours(5).Ticks, Description = "test", Location = "Milan", EventType = (int)WHEventType.Online });
        await client.CreateEventAsync(new CreateEventRequest { StartDateTime = DateTime.UtcNow.AddHours(3).Ticks, EndDateTime = DateTime.UtcNow.AddHours(5).Ticks, Description = "test", Location = "Milan", EventType = (int)(WHEventType.Babysitting | WHEventType.Fun) });

        var response = await client.GetEventsAsync(new GetEventFilterRequest { EventType = (int)WHEventType.Fun });

        // Assert
        response.Events.Should().HaveCount(4);
    }

    [Fact]
    public async void UpdateEvent1()
    {
        // Arrange
        var client = new WHEvent.WHEventClient(Channel);

        // Act
        var newEvent = await client.CreateEventAsync(new CreateEventRequest { StartDateTime = DateTime.UtcNow.AddHours(-1).Ticks, EndDateTime = DateTime.UtcNow.AddHours(1).Ticks, Description = "test", Location = "Milan", EventType = (int)WHEventType.Fun });

        var response = await client.UpdateEventAsync(new UpdateEventRequest { Id = newEvent.Id, Description = "updatedDesc" });

        // Assert
        response.Description.Should().Be("updatedDesc");
    }

    [Fact]
    public async void UpdateEvent2()
    {
        // Arrange
        var client = new WHEvent.WHEventClient(Channel);

        // Act
        var newEvent = await client.CreateEventAsync(new CreateEventRequest { StartDateTime = DateTime.UtcNow.AddHours(-1).Ticks, EndDateTime = DateTime.UtcNow.AddHours(1).Ticks, Description = "test", Location = "Milan", EventType = (int)WHEventType.Fun });

        var response = await client.UpdateEventAsync(new UpdateEventRequest { Id = newEvent.Id, Description = "updatedDesc2", EventType = (int)WHEventType.Babysitting });

        // Assert
        response.Description.Should().Be("updatedDesc2");
        response.EventType.Should().Be((int)WHEventType.Babysitting);

    }

}