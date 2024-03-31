namespace Api.Data.Infrastructure.Extensions
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata;
    using System.Text.RegularExpressions;

    internal static class ModelBuilderExtensions
    {
        internal static void DisableTableNamePlularlization(this ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.DisplayName());
            }
        }

        internal static void DisableCascadeDeletes(this ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        internal static void ApplyColumnNameConvention(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var properties = entityType.GetProperties().Where(x => !x.IsShadowProperty());

                foreach (var property in properties)
                {
                    var columnName = GetColumnName(property);
                    property.SetColumnName(columnName);
                }
            }
        }

        private static string GetColumnName(IMutableProperty property)
        {
            var result = Regex.Replace(property.GetColumnName(), ".[A-Z]", m => m.Value[0] + "_" + m.Value[1]);

            return result.ToLower();
        }
    }
}
