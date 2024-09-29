using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Utils.Common;
namespace Utils.Tools;
public static class ToolsModelBuilder
{

    //sample
    //Entity musst inheeit from BaseEntity
    //protected override void OnModelCreating(ModelBuilder builder)
    //{
    //    //builder.ConfigurationDb();
    //    //builder.ApplyConfigurationsEntity();

    //}
    public static void ConfigurationDb(this ModelBuilder modelBuilder)
    {
        var type = typeof(IEntity);
        var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => type.IsAssignableFrom(p) && !p.Name.Contains("BaseEntity") && p.IsPublic && p.IsClass);
        foreach (var item in types) modelBuilder.Entity(item);

    }
    public static void ApplyConfigurationsEntity(this ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
