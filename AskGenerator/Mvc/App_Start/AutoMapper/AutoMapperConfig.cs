using AutoMapper;
using AskGenerator.Business.Entities;
using AskGenerator.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AskGenerator.App_Start.AutoMapper
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            var types = Assembly.GetExecutingAssembly().GetExportedTypes();

            LoadStandartMappings(types);

            LoadCustomMappings(types);
        }

        #region private

        private static void LoadStandartMappings(IEnumerable<Type> types)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where i.IsGenericType &&
                            i.GetGenericTypeDefinition() == typeof(IMapFrom<>) &&
                            !t.IsAbstract &&
                            !t.IsInterface
                        select new
                        {
                            Source  = i.GetGenericArguments()[0],
                            Destination = t
                        }).ToArray(); 

            foreach(var map in maps)
            {                
                Mapper.CreateMap(map.Source, map.Destination).ReverseMap();
            }
        }

        private static void LoadCustomMappings(IEnumerable<Type> types)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces() 
                        where typeof(IHaveCustomMappings).IsAssignableFrom(t) &&
                            !t.IsAbstract &&
                            !t.IsInterface
                        select (IHaveCustomMappings)Activator.CreateInstance(t)).ToArray();

            foreach(var map in maps)
            {
                map.CreateMappings(Mapper.Configuration);
            }
        }

        #endregion
    }
}
