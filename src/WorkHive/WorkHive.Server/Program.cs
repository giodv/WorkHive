using WorkHive.Infrastructure.Persistence;
using WorkHive.Server.Services;

namespace WorkHive.Server;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}

//var builder = WebApplication.CreateBuilder(args);

//// Additional configuration is required to successfully run gRPC on macOS.
//// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

//// Add services to the container.
//builder.Services.AddGrpc();

//builder.Services.AddInfrastructureServices(builder.Configuration);
//builder.Services.AddApplicationServices();

//var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
//    // Initialise and seed database
//    using (var scope = app.Services.CreateScope())
//    {
//        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
//        await initialiser.InitialiseAsync();
//        await initialiser.SeedAsync();
//    }
//}

//// Configure the HTTP request pipeline.
//app.MapGrpcService<EventService>();
//app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

//app.Run();


//public partial class Program { }