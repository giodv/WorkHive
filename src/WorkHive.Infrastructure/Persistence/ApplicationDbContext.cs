﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WorkHive.Application.Common.Interfaces;
using WorkHive.Domain.Entities;
using WorkHive.Domain.Log;

namespace WorkHive.Infrastructure.Persistence;
public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly ILogger<ApplicationDbContext> _logger;

    public ApplicationDbContext(
        ILogger<ApplicationDbContext> logger,
       DbContextOptions<ApplicationDbContext> options)
       : base(options)
    {
        _logger = logger;
    }

    public DbSet<WHEvent> WHEvents => Set<WHEvent>();
    public DbSet<WHUser> WhUsers => Set<WHUser>();
    public DbSet<WHCompany> WhCompanies => Set<WHCompany>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<WHEvent>()
                .Property(x => x.LocationAttributes)
                .HasConversion(new ValueConverter<List<string>, string>(
                    v => JsonConvert.SerializeObject(v), // Convert to string for persistence
                    v => JsonConvert.DeserializeObject<List<string>>(v) ?? new())); // Convert to List<String> for use

        builder.Entity<WHEvent>()
            .HasMany(k => k.Guests)
            .WithMany(x => x.GuestEvents);

        builder.Entity<WHEvent>().Navigation(el => el.Guests).AutoInclude();

        builder.Entity<WHCompany>().HasMany(c => c.Users).WithOne(x => x.WhCompany).HasForeignKey(x => x.WhCompanyId);

        builder.Entity<WHEvent>()
            .HasOne(k => k.Owner)
            .WithMany(x => x.OwnerEvents)
            .HasForeignKey(x => x.OwnerId);

        builder.Entity<WHEvent>().Navigation(e => e.Owner).AutoInclude();


        base.OnModelCreating(builder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex)
        {
            _logger.ErrorWhileSavingChanges(ex);
            throw;
        }
    }
}
