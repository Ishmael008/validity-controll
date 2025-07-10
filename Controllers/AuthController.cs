
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
            if (userName == "Ishmael" && password == "ishmael2023")
            {
                var token = TokenService.GenerateToken(new DoMain.Models.UsuarioModel());
                return Ok(token);


            }

            return BadRequest("Usuario or name or email, invalid!");
        }
        private readonly IUsuarioRepository _usuarioRepository;

       

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
