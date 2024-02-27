using Microsoft.AspNetCore.Mvc;

namespace Product.Microservice.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/products")]
    [ApiController]
    public class ProductController : BaseController
    {
       
    }
}