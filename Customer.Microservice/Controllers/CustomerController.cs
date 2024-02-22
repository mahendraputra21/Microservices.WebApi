using Microsoft.AspNetCore.Mvc;
using Model;
using Services.Services;

namespace Customer.Microservice.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
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
                return BadRequest("Error when inserting data");

            return Ok("Customer ID " + customerId + " Created Successfully!");
        }
    }
}
