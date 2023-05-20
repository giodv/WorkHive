using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using WorkHive.Application.Common.Interfaces;
using WorkHive.Domain.Entities;

namespace WorkHive.Infrastructure.Persistence;
public class ApplicationDbContext : DbContext, IApplicationDbContext
{

    public ApplicationDbContext(
       DbContextOptions<ApplicationDbContext> options)
       : base(options)
    {
    }

    public DbSet<WHEvent> WHEvents => Set<WHEvent>();
    public DbSet<WHUser> WhUsers => Set<WHUser>();
    public DbSet<WHCompany> WhCompanies => Set<WHCompany>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<WHEvent>()
                .Property(x => x.GuestIds)
                .HasConversion(new ValueConverter<List<Guid>, string>(
                    v => JsonConvert.SerializeObject(v), // Convert to string for persistence
                    v => JsonConvert.DeserializeObject<List<Guid>>(v))); // Convert to List<String> for use

        builder.Entity<WHEvent>()
                .Property(x => x.Attributes)
                .HasConversion(new ValueConverter<List<string>, string>(
                    v => JsonConvert.SerializeObject(v), // Convert to string for persistence
                    v => JsonConvert.DeserializeObject<List<string>>(v))); // Convert to List<String> for use

        builder.Entity<WHEvent>().HasMany(k => k.GuestIds).WithMany(x => x.GuestEvents);

        base.OnModelCreating(builder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}
