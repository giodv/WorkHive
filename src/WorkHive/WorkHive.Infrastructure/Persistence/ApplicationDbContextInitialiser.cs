﻿using Microsoft.Extensions.Logging;
using WorkHive.Core;
using WorkHive.Core.Entities;

namespace WorkHive.Infrastructure.Persistence;
public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            //if (_context.Database.IsSqlServer())
            //{
            //    await _context.Database.MigrateAsync();
            //}
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default data
        // Seed, if necessary
        if (!_context.WHEvents.Any())
        {
            _context.WHEvents.Add(new WHEventEntity
            {
                Id = Guid.NewGuid(),
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddHours(9),
                Description = "Test",
                EventType = WHEventType.Work | WHEventType.Fun,
                GuestIds = new List<WHEventGuestEntity>(),
                Location = "Napoli, Via Brombeis",
                MaxGuest = 12,
                OrganizerId = Guid.NewGuid()
            });

            await _context.SaveChangesAsync();
        }
    }
}