using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkHive.Domain;
using WorkHive.Domain.Entities;

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
            if (_context.Database.IsNpgsql())
            {
                if (_context.Database.GetPendingMigrations().Any())
                {
                    await _context.Database.MigrateAsync();
                }
            }
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
        var companyId = Guid.Parse("B4B117AA-3AD8-4D13-802A-B8BAD0DC8E94");

        if (!_context.WhCompanies.Any())
        {
            _context.WhCompanies.Add(new WHCompany
            {
                Id = companyId,
                Name = "TrimOni Corp"
            });

            await _context.SaveChangesAsync();
        }

        var userId = Guid.Parse("B4B117AA-3AD8-4D13-802A-B8BAD0DC8E95");

        if (!_context.WhUsers.Any())
        {
            _context.WhUsers.Add(new WHUser
            {
                Id = userId,
                WhCompanyId = companyId,
            });

            await _context.SaveChangesAsync();
        }

        if (!_context.WHEvents.Any())
        {
            _context.WHEvents.Add(new WHEvent
            {
                Description = "Test Event",
                EventAttributes = WHEventType.WorkAndFun,
                Location = "Naploli, Via Brombeis",
                LocationAttributes = new List<string> { "Locale fumatori", "Posteggio moto" },
                MaxGuest = 12,
                OwnerId = userId,
                StartDate = DateTime.Now.AddDays(1),
                EndDate = DateTime.Now.AddDays(2)
            });

            await _context.SaveChangesAsync();
        }

    }
}