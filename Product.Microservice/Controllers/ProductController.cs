using Common.Constants;
using Common.Helpers;
using Microsoft.AspNetCore.Mvc;
using Model;
using ModelValidator;
using Services.Services;

namespace Product.Microservice.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/products")]
    [ApiController]
    public class ProductController : BaseController
    {
       private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductDTO request)
        {
            ApiResponseHelper apiResponseHelper = new();
            ProductDTOValidator validator = new();
            
            //validation checking 
            if (!validator.Validate(request).IsValid)
            {
                string? errorMessage = validator.Validate(request).Errors
                                                .FirstOrDefault()?.ErrorMessage;
                
                var errorContent = apiResponseHelper.ApiErrorResponse(
                    errorMessage,
                    HttpContext.Request.Path);
                return BadRequest(errorContent);
            }

            await _productService.InsertProductAsync(request);
            var successContent = apiResponseHelper
                .ApiSuccessResponse(Message.PRODUCT_CREATED_SUCCESSFULLY);

            return Ok(successContent);
        }
    }
}