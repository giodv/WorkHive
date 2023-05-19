using Microsoft.EntityFrameworkCore;
using WorkHive.Infrastructure.Persistence;
using WorkHive.Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();


builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();

var app = builder.Build();

app.Logger.LogInformation("test");
// Configure the HTTP request pipeline.
app.MapGrpcService<EventService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    app.Logger.LogInformation("test scope");


    var context = services.GetRequiredService<ApplicationDbContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        app.Logger.LogInformation("test migration");

        context.Database.Migrate();
    }
}

app.Run();


public partial class Program { }