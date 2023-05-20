using Microsoft.EntityFrameworkCore;
using WorkHive.Domain.Entities;

namespace WorkHive.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<WHEvent> WHEvents { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}