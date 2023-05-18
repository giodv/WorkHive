﻿using Grpc.Net.Client;
using Microsoft.Extensions.Logging;
using WorkHive.Server;
using WotkHive.UnitTest.IntegrationTests.Helpers;
using Xunit.Abstractions;

namespace WotkHive.UnitTest.IntegrationTests;
public class IntegrationTestBase : IClassFixture<GrpcTestFixture<Startup>>, IDisposable
{
    private GrpcChannel? _channel;
    private IDisposable? _testContext;

    protected GrpcTestFixture<Startup> Fixture { get; set; }

    protected ILoggerFactory LoggerFactory => Fixture.LoggerFactory;

    protected GrpcChannel Channel => _channel ??= CreateChannel();

    protected GrpcChannel CreateChannel()
    {
        return GrpcChannel.ForAddress("http://localhost", new GrpcChannelOptions
        {
            LoggerFactory = LoggerFactory,
            HttpHandler = Fixture.Handler
        });
    }

    public IntegrationTestBase(GrpcTestFixture<Startup> fixture, ITestOutputHelper outputHelper)
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