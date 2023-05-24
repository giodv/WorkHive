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

    [Fact]
    public async void UpdateEvent3()
    {
        // Arrange
        var client = new WHEvent.WHEventClient(Channel);

        // Act
        CreateEventRequest createRequest = new CreateEventRequest { StartDateTime = DateTime.UtcNow.AddHours(-1).Ticks, EndDateTime = DateTime.UtcNow.AddHours(1).Ticks, Description = "test", Location = "Milan", EventType = (int)WHEventType.Fun };
        createRequest.Attributes.Add("Area Fumatori");
        var newEvent = await client.CreateEventAsync(createRequest);

        UpdateEventRequest request = new UpdateEventRequest { Id = newEvent.Id, Description = "updatedDesc2", EventType = (int)WHEventType.Babysitting };
        request.Attributes.Add("Area Non Fumatori");
        var response = await client.UpdateEventAsync(request);

        // Assert
        response.Description.Should().Be("updatedDesc2");
        response.EventType.Should().Be((int)WHEventType.Babysitting);
        response.Attributes.Should().HaveCount(1);
        response.Attributes.ElementAt(0).Should().Be("Area Non Fumatori");

    }


    [Fact]
    public async void GetEvent1()
    {
        // Arrange
        var client = new WHEvent.WHEventClient(Channel);

        // Act
        var newEvent = await client.CreateEventAsync(new CreateEventRequest { StartDateTime = DateTime.UtcNow.AddHours(-1).Ticks, EndDateTime = DateTime.UtcNow.AddHours(1).Ticks, Description = "test", Location = "Milan", EventType = (int)WHEventType.Fun });

        var response = await client.GetEventAsync(new GetEventRequest { Id = newEvent.Id });

        // Assert
        response.Should().NotBeNull();

    }

    [Fact]
    public async void JoinEvent1()
    {
        // Arrange
        var client = new WHEvent.WHEventClient(Channel);

        // Act
        var newEvent = await client.CreateEventAsync(new CreateEventRequest { StartDateTime = DateTime.UtcNow.AddHours(-1).Ticks, EndDateTime = DateTime.UtcNow.AddHours(1).Ticks, Description = "test", Location = "Milan", EventType = (int)WHEventType.Fun });

        await client.JoinEventAsync(new JoinEventRequest { Id = newEvent.Id });
        await client.JoinEventAsync(new JoinEventRequest { Id = newEvent.Id });


        var response = await client.GetEventAsync(new GetEventRequest { Id = newEvent.Id });

        // Assert
        response.Should().NotBeNull();

    }

    [Fact]
    public async void CreateFakeEvent1()
    {
        // Arrange
       var client = new WHEvent.WHEventClient(Channel);

        // Act
        var newEvent = await client.CreateFakeEventAsync(new CreateFakeEventRequest() );

        var response = await client.GetEventAsync(new GetEventRequest { Id = newEvent.Id });

        // Assert
        response.Should().NotBeNull();

        response.Attributes.Should().NotBeNull().And.HaveCount(3);
    }

}