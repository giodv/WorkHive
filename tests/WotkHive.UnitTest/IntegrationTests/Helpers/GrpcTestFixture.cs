using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WotkHive.UnitTest.IntegrationTests.Helpers;
using Xunit.Abstractions;

namespace WotkHive.Tests.IntegrationTests.Helpers;
public delegate void LogMessage(LogLevel logLevel, string categoryName, EventId eventId, string message, Exception? exception);

public class GrpcTestFixture<TStartup> : IDisposable where TStartup : class
{
    private TestServer? _server;
    private IHost? _host;
    private HttpMessageHandler? _handler;
    private Action<IWebHostBuilder>? _configureWebHost;
    private WebApplicationFactory<Program> _appFactory;

    public event LogMessage? LoggedMessage;

    public GrpcTestFixture()
    {
        LoggerFactory = new LoggerFactory();
        LoggerFactory.AddProvider(new ForwardingLoggerProvider((logLevel, category, eventId, message, exception) =>
        {
            LoggedMessage?.Invoke(logLevel, category, eventId, message, exception);
        }));

        _appFactory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddSingleton<ILoggerFactory>(LoggerFactory);
                });
            });


    }

    public void ConfigureWebHost(Action<IWebHostBuilder> configure)
    {
        _configureWebHost = configure;
    }

    private void EnsureServer()
    {
        if (_host == null)
        {
            var builder = new HostBuilder()
                .ConfigureServices(services =>
                {
                    services.AddSingleton<ILoggerFactory>(LoggerFactory);
                })
                .ConfigureWebHostDefaults(webHost =>
                {
                    webHost
                        .UseTestServer()
                        .UseStartup<TStartup>();

                    _configureWebHost?.Invoke(webHost);
                });
            _host = builder.Start();
            _server = _host.GetTestServer();
            _handler = _server.CreateHandler();
        }
    }


    public LoggerFactory LoggerFactory { get; }

    public WebApplicationFactory<Program> AppFactory => _appFactory;

    public HttpMessageHandler Handler
    {
        get
        {
            EnsureServer();
            return _handler!;
        }
    }

    public void Dispose()
    {
        _handler?.Dispose();
        _host?.Dispose();
        _server?.Dispose();
    }

    public IDisposable GetTestContext(ITestOutputHelper outputHelper)
    {
        return new GrpcTestContext<TStartup>(this, outputHelper);
    }
}