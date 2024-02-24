using Common.Constants;
using Microsoft.AspNetCore.Mvc;
using Model;
using ModelValidator;
using Services.Services;

namespace Customer.Microservice.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/customer")]
    [ApiController]
    public class CustomerController : BaseController
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        #region Private Method
        private IActionResult CreateCustomerErrorResponse(string? errorMessage)
        {
            errorContent.ErrorResponse.Title = Message.ERROR_TITLE;
            errorContent.ErrorResponse.Status = (int?)Enums.NotificationType.BADREQUEST;
            errorContent.ErrorResponse.Detail = errorMessage;
            errorContent.ErrorResponse.Instances = HttpContext.Request.Path;
            return BadRequest(errorContent);
        }

        private IActionResult CreateCustomerSuccessResponse(int customerId)
        {
            content.Response.Success = true;
            content.Response.Message = Message.CUSTOMER_CREATED_SUCCESSFULLY;
            content.Response.Data = new { customerId };
            return Ok(content);
        }
        #endregion

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CustomerDTO request)
        {
            // Validation Checking
            var validator = new CustomerDTOValidator();
            if (!validator.Validate(request).IsValid)
            {
                string? errorMessage = validator.Validate(request).Errors.FirstOrDefault()?.ErrorMessage;
                return CreateCustomerErrorResponse(errorMessage);

            }

            var customerId = await _customerService.InsertCustomerAsync(request);
            return CreateCustomerSuccessResponse(customerId);

        }
    }
}
