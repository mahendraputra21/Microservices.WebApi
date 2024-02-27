using Infrastructure.Repositories;
using Model;

namespace Services.Services
{
    public interface IProductService
    {
        Task<int> InsertProductAsync(ProductDTO productDTO);
    }

    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<int> InsertProductAsync(ProductDTO productDTO)
        {
            productDTO.ProductGUID = Guid.NewGuid().ToString();
            return await _productRepository.InsertProductAsync(productDTO);
        }
    }
}
