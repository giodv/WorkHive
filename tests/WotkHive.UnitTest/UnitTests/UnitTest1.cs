using Microsoft.Extensions.Logging;
using Moq;
using WorkHive.Server.Services;

namespace WotkHive.UnitTest;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var mockLogger = new Mock<ILogger<EventService>>();
        ILogger<EventService> logger = mockLogger.Object;

        //var service = new EventService(logger,);
    }
}