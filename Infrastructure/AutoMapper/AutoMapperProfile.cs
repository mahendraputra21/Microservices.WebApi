using AutoMapper;
using Domain;
using Model;

namespace Infrastructure.AutoMapper
{
    public interface IAutoMapperProfile
    {
        public IMapper Configuration { get; }
    }

    public class AutoMapperProfile : Profile, IAutoMapperProfile
    {
        public AutoMapperProfile()
        {
            CreateMap<Customer, CustomerDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
        }

        public IMapper Configuration => new MapperConfiguration(cfg => cfg.AddProfile(this)).CreateMapper();
    }
}
