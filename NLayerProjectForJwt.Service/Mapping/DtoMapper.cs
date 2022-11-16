using AutoMapper;
using NLayerProjectForJwt.Core.Dtos;
using NLayerProjectForJwt.Core.Dtos.Product;
using NLayerProjectForJwt.Core.Entities;

namespace NLayerProjectForJwt.Service.Mapping
{
    internal class DtoMapper : Profile
    {
        public DtoMapper()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<UserAppDto, UserApp>().ReverseMap();
        }
    }
}
