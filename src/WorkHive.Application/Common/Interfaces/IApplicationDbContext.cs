using Microsoft.EntityFrameworkCore;
using WorkHive.Core.Entities;

namespace WorkHive.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<WHEventEntity> WHEvents { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}