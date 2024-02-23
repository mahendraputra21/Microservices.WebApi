using Common.Constants;
using Microsoft.AspNetCore.Mvc;
using Model;
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

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CustomerDTO request)
        {
            var customerId = await _customerService.InsertCustomerAsync(request);
            if (customerId < 1)
            {
                content.Response.Success = false;
                content.Response.Message = "Create Customer Fail!";
                content.Response.Error_Code = (int?)Enums.NotificationType.BADREQUEST;
                content.Response.Data = null;
                return BadRequest(content);
            }

            content.Response.Success = true;
            content.Response.Message = Message.CUSTOMER_CREATED_SUCCESSFULLY;
            content.Response.Error_Code = null;
            content.Response.Data = new { customerId };
            return Ok(content);
            
        }
    }
}
