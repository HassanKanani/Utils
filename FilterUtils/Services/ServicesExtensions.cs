using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Utils.Common;
namespace Utils.Services;
public static class ServicesExtensions
{
    public static void ExtenalServicesExtention(this IServiceCollection service, IConfiguration configuration, Assembly[] CurrentDomainAssembly)
    {
        #region IOC Container AutoFact
        //service.AddScoped(typeof(QueryableExtensions<>));
        //service.AddScoped(typeof(FilterServices<>));
        #region scopeRegiste
        Type scopedRegistration = typeof(ScopedRegistrationAttribute);
        var scopedtypes = CurrentDomainAssembly
          .SelectMany(s => s.GetTypes())
          .Where(p => p.IsDefined(scopedRegistration, true) && !p.IsInterface).Select(s => new
          { Service = s.GetInterface($"I{s.Name}"), Implementation = s }).Where(x => x.Service != null);
        foreach (var type in scopedtypes) { service.AddScoped(type.Service, type.Implementation); }

        #endregion
        #region AddSingleton
        Type SingletonRegistration = typeof(SingletonRegistrationAttribute);
        var Singletontypes = CurrentDomainAssembly
          .SelectMany(s => s.GetTypes())
          .Where(p => p.IsDefined(SingletonRegistration, true) && !p.IsInterface).Select(s => new
          { Service = s.GetInterface($"I{s.Name}"), Implementation = s }).Where(x => x.Service != null);
        foreach (var type in Singletontypes) { service.AddSingleton(type.Service, type.Implementation); }

        #endregion
        #region TransientRegiste
        Type TransientRegistration = typeof(TransientRegistrationAttribute);
        var Transienttypes = CurrentDomainAssembly
          .SelectMany(s => s.GetTypes())
          .Where(p => p.IsDefined(TransientRegistration, true) && !p.IsInterface).Select(s => new
          { Service = s.GetInterface($"I{s.Name}"), Implementation = s }).Where(x => x.Service != null);
        foreach (var type in Transienttypes) { service.AddSingleton(type.Service, type.Implementation); }


        #endregion

        #endregion
        #region Other Srvice
        service.AddAutoMapper(typeof(Automapper.MappingProfile));
        #endregion
    }

}
