using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WorkHive.Application.Common.Interfaces;
using WorkHive.Infrastructure.Persistence;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        //if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        //{
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("CleanArchitectureDb"));
        //}
        //else
        //{
        //    throw new InvalidOperationException("Not implemented");
        //    //services.AddDbContext<ApplicationDbContext>(options =>
        //    //    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
        //    //        builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        //}

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ApplicationDbContextInitialiser>();
        
        return services;
    }
}