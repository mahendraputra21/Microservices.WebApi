using Common.Constants;
using Common.Helpers;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Model;
using ModelValidator;
using Services.Services;

namespace Customer.Microservice.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/customers")]
    [ApiController]
    public class CustomerController : BaseController
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CustomerDTO request)
        {
            
            CustomerDTOValidator validator = new();
            ApiResponseHelper apiResponseHelper = new();
            
            // Validation Checking
            if (!validator.Validate(request).IsValid)
            {
                string? errorMessage = validator.Validate(request).Errors
                                               .FirstOrDefault()?.ErrorMessage;

                var errorContent = apiResponseHelper.ApiErrorResponse(
                    errorMessage,
                    HttpContext.Request.Path);
                return BadRequest(errorContent);
            }

            await _customerService.InsertCustomerAsync(request);
            var successContent = apiResponseHelper
                .ApiSuccessResponse(Message.PRODUCT_CREATED_SUCCESSFULLY);

            return Ok(successContent);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer([FromForm] CustomerDTO request, int id)
        {
            await _customerService.UpdateCustomerAsync(request, id);
            
            ApiResponseHelper apiResponseHelper = new();
            var successContent = apiResponseHelper
               .ApiSuccessResponse(Message.CUSTOMER_UPDATED_SUCCESSFULLY);

            return Ok(successContent);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerAsync(int id)
        {
            await _customerService.DeleteCustomerAsync(id);

            ApiResponseHelper apiResponseHelper = new();
            var successContent = apiResponseHelper
              .ApiSuccessResponse(Message.CUSTOMER_DELETE_SUCCESSFULLY);

            return Ok(successContent);
        }

        [HttpGet()]
        public async Task<IActionResult> GetCustomersAsync()
        {
            var customers = await _customerService.GetCustomersAsync();

            ApiResponseHelper apiResponseHelper = new();
            var successContent = apiResponseHelper
             .ApiGetSuccessResponse(customers);
            return Ok(successContent);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerByIdAsync(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);

            ApiResponseHelper apiResponseHelper = new();
            var successContent = apiResponseHelper
             .ApiGetSuccessResponse(customer);
            return Ok(successContent);
        }
    }
}
