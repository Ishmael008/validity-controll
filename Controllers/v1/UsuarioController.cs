using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ValidityControl.Application.ViewModel;
using ValidityControl.DoMain.Models;
using ValidityControl.Infraestrutura;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ValidityControl.Controllers.v1
{
    [ApiController]
    [Route("api/{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        private readonly ILogger<UsuarioController> _logger;


        public UsuarioController(IUsuarioRepository usuarioRepository, ILogger<UsuarioController> logger)
        {
            _usuarioRepository = usuarioRepository ?? throw new ArgumentNullException(nameof(usuarioRepository));
            _logger = logger;
        }


        [HttpPost]
        public IActionResult Add(UsuarioModelViewModel usuarioViewModel)
        {


            var usuario = new UsuarioModel(usuarioViewModel.Name, usuarioViewModel.Password);

            _usuarioRepository.Add(usuario);

            if (usuario == null)
                return BadRequest("Usuário não pode ser nulo");

            if (string.IsNullOrEmpty(usuario.name))
                return BadRequest("O campo name é obrigatório");


            if (string.IsNullOrEmpty(usuario.password))
                return BadRequest("O campo name é obrigatório");

            return Ok(new { message = "Usuário cadastrado com sucesso!" });


        }



        

    

        [HttpGet("validar")]
        public IActionResult Validar(string name, string password)
        {
            var usuario = _usuarioRepository.GetByNameAndPassword(name, password);

            if (usuario != null)
            {
                return Ok(true);
            }

            else return NotFound();

       
       

        }



        [HttpDelete]
        public async Task<ActionResult<UsuarioModel>> Delete(int id)
        {
            bool delatado = await _usuarioRepository.Delete(id);
            return Ok(delatado);

        }



    }

}


