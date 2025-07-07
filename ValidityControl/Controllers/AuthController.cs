using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Diagnostics.Eventing.Reader;
using ValidityControl.Infraestrutura;
using ValidityControl.DoMain.Models;
using ValidityControl.Application.ViewModel;
using ValidityControl.Controllers.Service;

namespace ValidityControl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
       /* private readonly IUsuarioRepository _usuarioAuth;

        public AuthController(IUsuarioRepository usuarioAuth)
        {
            _usuarioAuth = usuarioAuth ?? throw new ArgumentNullException(nameof(usuarioAuth));
        }
       */


        [HttpPost]
        public IActionResult Auth(string userName, string password)
        {
            //var usuarioAuth = new UsuarioModel(usuarioViewModel.Id, usuarioViewModel.Name, usuarioViewModel.Email);
            if(userName == "Ishmael" && password == "ishmael2023")
            {
                var token = TokenService.GenerateToken(new DoMain.Models.UsuarioModel());
                return Ok(token);

                
            }
            
            return BadRequest("Usuario or name or email, invalid!");
        }
    }
}
