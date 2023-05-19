using Microsoft.EntityFrameworkCore;
using WorkHive.Domain.Entities;

namespace WorkHive.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<WHEventEntity> WHEvents { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}