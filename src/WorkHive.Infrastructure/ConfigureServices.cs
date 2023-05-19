using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WorkHive.Application.Common.Interfaces;
using WorkHive.Infrastructure.Persistence;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase("CleanArchitectureDb"));

        //services.AddDbContext<ApplicationDbContext>(options =>
        //options.UseNpgsql("User ID=bloguser;Password=bloguser;Host=postgres_image;Port=5432;Database=blogdb;Pooling=true;"));

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        return services;
    }
}