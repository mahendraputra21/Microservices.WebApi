using Common.Constants;
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

        #region Private Method
        private IActionResult CreateCustomerErrorResponse(string? errorMessage)
        {
            errorContent.ErrorResponse.Title = Message.ERROR_TITLE;
            errorContent.ErrorResponse.Status = (int?)Enums.NotificationTypes.BADREQUEST;
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
        private IActionResult UpdateCustomerSuccessResponse(bool isUpdate)
        {
            content.Response.Success = true;
            content.Response.Message = Message.CUSTOMER_UPDATED_SUCCESSFULLY;
            content.Response.Data = new { isUpdate };
            return Ok(content);
        }
        private IActionResult DeleteCustomerSuccessResponse(bool isDelete)
        {
            content.Response.Success = true;
            content.Response.Message = Message.CUSTOMER_DELETE_SUCCESSFULLY;
            content.Response.Data = new { isDelete };
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer([FromForm] CustomerDTO request, int id)
        {
            var isUpdated = await _customerService.UpdateCustomerAsync(request, id);
            return UpdateCustomerSuccessResponse(isUpdated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerAsync(int id)
        {
            var isDeleted = await _customerService.DeleteCustomerAsync(id);
            return DeleteCustomerSuccessResponse(isDeleted);
        }

        [HttpGet()]
        public async Task<IActionResult> GetCustomersAsync()
        {
            var customers = await _customerService.GetCustomersAsync();

            content.Response.Success = true;
            content.Response.Message = Message.OK_MESSAGE;
            content.Response.Data = new { customers };
            return Ok(content);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerByIdAsync(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);

            content.Response.Success = true;
            content.Response.Message = Message.OK_MESSAGE;
            content.Response.Data = new { customer };
            return Ok(content);
        }
    }
}
