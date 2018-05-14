using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace DimasikStore.Mvc.EntityMapper
{
    public class MapperConfig
    {
        public static void Initialize()
        {
            var profiles = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.ExportedTypes)
                    .Where(t => typeof(Profile).GetTypeInfo().IsAssignableFrom(t.GetTypeInfo()))
                    .Where(t => !t.GetTypeInfo().IsAbstract);

            Mapper.Initialize(cfg =>
            {
                foreach (var profile in profiles)
                    cfg.AddProfile(profile);
            });
        }
    }
}