using Microsoft.EntityFrameworkCore;
using WorkHive.Application.Common.Interfaces;
using WorkHive.Core.Entities;

namespace WorkHive.Infrastructure.Persistence;
public class ApplicationDbContext : DbContext, IApplicationDbContext
{

    public ApplicationDbContext(
       DbContextOptions<ApplicationDbContext> options)
       : base(options)
    {
    }

    public DbSet<WHEventEntity> WHEvents => Set<WHEventEntity>();


    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}
