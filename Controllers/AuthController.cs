using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;
using ValidityControl.Application.ViewModel;
using ValidityControl.Controllers.Service;
using ValidityControl.DoMain.Models;
using ValidityControl.Infraestrutura;
using ValidityControl.Infraestrutura.Repositories;

namespace ValidityControl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public AuthController(IUsuarioRepository usuario)
        {
            _usuarioRepository = usuario;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginViewModel login)
        {
            var usuario = _usuarioRepository.GetByNameAndPassword(login.Name, login.Password);

            if (usuario == null)
                return Unauthorized("Credenciais inválidas");

            var token = TokenService.GenerateToken(usuario);
            return Ok(token);
        }
    }
}
