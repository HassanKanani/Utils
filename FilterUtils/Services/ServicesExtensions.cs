using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repositories;
using Utils.Common;
using Utils.Contracts;
namespace Utils.Services;
public static class ServicesExtensions
{
    public static void ExtenalServicesExtention(this IServiceCollection service, IConfiguration configuration)
    {
        #region IOC Container AutoFact
        service.AddScoped(typeof(IRepository<>), typeof(Repository<,>));
        service.AddScoped(typeof(IService<,,,,>), typeof(Service<,,,,>));
        Type scopedRegistration = typeof(ScopedRegistrationAttribute);
        var types = AppDomain.CurrentDomain.GetAssemblies()
          .SelectMany(s => s.GetTypes())
          .Where(p => p.IsDefined(scopedRegistration, true) && !p.IsInterface).Select(s => new
          { Service = s.GetInterface($"I{s.Name}"), Implementation = s }).Where(x => x.Service != null);
        foreach (var type in types) { service.AddScoped(type.Service, type.Implementation); }
        #endregion
        #region Other Srvice
        service.AddAutoMapper(typeof(Automapper.MappingProfile));
        #endregion
    }

}
