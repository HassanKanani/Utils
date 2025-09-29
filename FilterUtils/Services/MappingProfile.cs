using AutoMapper;
using Utils.Models;
namespace Automapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        var type = typeof(IMapMark);
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => type.IsAssignableFrom(p) && p.IsClass && p.IsPublic);

        foreach (var entity in types)
        {
            var mapInterface = entity.GetInterfaces()
                .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IAutoMapper<>));

            if (mapInterface == null) continue;

            var sourceType = mapInterface.GetGenericArguments()[0];
            var destinationType = entity;

            CreateMap(sourceType, destinationType).ReverseMap();
        }
    }
}