using AutoMapper;
using NLayerProjectForJwt.Service.Mapping;
using System;

namespace NLayerProjectForJwt.Service
{
    public static class ObjectMapper
    {
        //uygulama ayağa kalktığıjnda bellekte olmayacak ne zaman çağırırsak o zaman yüklenecek
        private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(config =>
            {
                config.AddProfile<DtoMapper>();
            });

            return config.CreateMapper();
        });

        public static IMapper Mapper => lazy.Value;
    }
}
