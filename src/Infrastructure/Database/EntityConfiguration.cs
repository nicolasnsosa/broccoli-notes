using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Infrastructure.Database.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public interface IEntityConfiguration
{
    void Configure(ModelBuilder modelBuilder);
}

public class EntityConfiguration<TEntity> : IEntityConfiguration where TEntity : class
{
    public void Configure(ModelBuilder modelBuilder)
    {
        var entity = modelBuilder.Entity<TEntity>();

        var type = typeof(TEntity);
        var properties = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        foreach (var property in properties)
        {
            if (property.Name.Equals(Constants.ID_FIELD))
            {
                entity.HasKey(property.Name);
                continue;
            }
            if (property.Name.Equals(Constants.CREATED_DATE_FIELD))
            {
                entity.Property(property.Name).IsRequired();
                continue;
            }
            if (property.Name.Equals(Constants.UPDATED_DATE_FIELD))
            {
                entity.Property(property.Name);
                continue;
            }

            var attributes = property.GetCustomAttributes(false);
            var isNotMapped = attributes.OfType<NotMappedAttribute>().Any();
            if (isNotMapped)
                continue;

            var isNullable = Nullable.GetUnderlyingType(property.PropertyType) != null;
            entity.Property(property.Name).IsRequired(isNullable);
        }
    }
}