using AutoMapper;
using Utils.Models;
namespace Automapper
{
    public class MappingProfile : Profile
    {
        private const string Value = "IAutoMapper`2";

        public MappingProfile()
        {
            var type = typeof(IMapMark);
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => type.IsAssignableFrom(p) && p.IsPublic && p.IsClass);
            foreach (var entity in types)
            {
                List<Type> res = entity.GetInterfaces().First(n => n.Name.Equals(Value)).GetGenericArguments().ToList();
                var f = res[0];
                var s = res[1];
                CreateMap(f, s).ReverseMap();
            }

        }
    }
}