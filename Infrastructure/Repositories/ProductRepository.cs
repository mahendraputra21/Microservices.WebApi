using AutoMapper;
using Domain;
using Infrastructure.AutoMapper;
using Model;

namespace Infrastructure.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<int> InsertProductAsync(ProductDTO productDTO);
    }

    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly IMapper _mapper;

        public ProductRepository(ApplicationDbContext db,IAutoMapperProfile autoMapperProfile) : base(db)
        {
            _mapper = autoMapperProfile.Configuration;
        }

        public async Task<int> InsertProductAsync(ProductDTO productDTO)
        {
            var product = _mapper.Map<Product>(productDTO);
            await InsertAsync(product);
            return product.Id;
        }
    }
}
