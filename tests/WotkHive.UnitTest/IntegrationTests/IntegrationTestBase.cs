using Grpc.Net.Client;
using Microsoft.Extensions.Logging;
using WotkHive.Tests.IntegrationTests.Helpers;
using WotkHive.UnitTest.IntegrationTests.Helpers;
using Xunit.Abstractions;

namespace WotkHive.UnitTest.IntegrationTests;
public class IntegrationTestBase : IClassFixture<GrpcTestFixture<Program>>, IDisposable
{
    private GrpcChannel? _channel;
    private IDisposable? _testContext;

    protected GrpcTestFixture<Program> Fixture { get; set; }

    protected ILoggerFactory LoggerFactory => Fixture.LoggerFactory;

    protected GrpcChannel Channel => _channel ??= CreateChannel();

    protected GrpcChannel CreateChannel()
    {
        return GrpcChannel.ForAddress("http://localhost", new GrpcChannelOptions
        {
            LoggerFactory = LoggerFactory,
            HttpHandler = Fixture.AppFactory.Server.CreateHandler()
        });
    }

    public IntegrationTestBase(GrpcTestFixture<Program> fixture, ITestOutputHelper outputHelper)
    {
        Fixture = fixture;
        _testContext = Fixture.GetTestContext(outputHelper);
    }

    public void Dispose()
    {
        _testContext?.Dispose();
        _channel = null;
    }
}