using Microsoft.AspNetCore.Mvc;

namespace ValidityControl.Controllers.v1
{
    
   
        [ApiController]
        [Route("/")]
        public class HomeController : ControllerBase
        {
            [HttpGet]
            public IActionResult Get() => Ok("API do ValidityControl está rodando!");
        }

    
}
