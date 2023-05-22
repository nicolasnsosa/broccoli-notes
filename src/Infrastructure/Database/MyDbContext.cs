using System.Globalization;
using Infrastructure.Database.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public class MyDbContext : DbContext
{
    private readonly IEnumerable<IEntityConfiguration> _entityConfigurations;

    public MyDbContext(DbContextOptions<MyDbContext> options, IEnumerable<IEntityConfiguration> entityConfigurations)
        : base(options)
    {
        _entityConfigurations = entityConfigurations;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var configuration in _entityConfigurations)
        {
            configuration.Configure(modelBuilder);
        }
    }

    public async Task<int> SaveChangesAsync()
    {
        // Get all Added/Deleted/Modified entities (not Unmodified or Detached)
        foreach (var e in ChangeTracker.Entries()
                     .Where(p => p.State == EntityState.Added || p.State == EntityState.Modified))
        {
            if (e.State == EntityState.Added)
            {
                if (e.Entity.GetType().GetProperty(Constants.CREATED_DATE_FIELD) != null)
                {
                    var createDate = e.Property(Constants.CREATED_DATE_FIELD);
                    createDate.CurrentValue = DateTime.UtcNow;
                }
            }
            else if (e.State == EntityState.Modified)
            {
                if (e.Entity.GetType().GetProperty(Constants.UPDATED_DATE_FIELD) != null)
                {
                    var editDate = e.Property(Constants.UPDATED_DATE_FIELD);
                    editDate.CurrentValue = DateTime.UtcNow;
                }
            }
        }

        return await base.SaveChangesAsync();
    }
}