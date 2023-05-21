using Microsoft.EntityFrameworkCore;
using WorkHive.Domain.Entities;

namespace WorkHive.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<WHEvent> WHEvents { get; }

    DbSet<WHUser> WhUsers { get; }
    DbSet<WHCompany> WhCompanies { get; }


    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}