
﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Diagnostics.Eventing.Reader;
using ValidityControl.Infraestrutura;
using ValidityControl.DoMain.Models;
using ValidityControl.Application.ViewModel;
using ValidityControl.Controllers.Service;
﻿using Microsoft.AspNetCore.Connections;
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
        public readonly ILogger<AuthController> _logger;

        public AuthController(IUsuarioRepository usuarioRepository, ILogger<AuthController> logger)
        {
            _usuarioRepository = usuarioRepository;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UsuarioModel>> Login([FromBody] LoginViewModel login)
        {
            try
            {
                var usuario = await _usuarioRepository.GetByNameAndPassword(login.Name, login.Password);

                if (usuario == null)
                    return Unauthorized
                        ("Credenciais inválidas");

                var token = TokenService.GenerateToken(usuario);

                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "O sistema está desconectado. Tente mais tarde!");
            }
        }
    }

}
