using Common;
using Microsoft.AspNetCore.Mvc;

namespace Product.Microservice.Controllers
{
    public class BaseController : ControllerBase
    {
        public readonly ResponseSingleton content = ResponseSingleton.Instance;
        public readonly ErrorResponseSingleton errorContent = ErrorResponseSingleton.Instance; 
    }
}
