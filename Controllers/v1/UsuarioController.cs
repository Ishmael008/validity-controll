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
        public IActionResult Add([FromForm] UsuarioModelViewModel usuarioViewModel)
        {
            if (usuarioViewModel == null)
                return BadRequest("Dados inválidos.");

            if (string.IsNullOrEmpty(usuarioViewModel.Name))
                return BadRequest("O campo name é obrigatório");

            if (string.IsNullOrEmpty(usuarioViewModel.Password))
                return BadRequest("O campo password é obrigatório");

            var usuario = new UsuarioModel(usuarioViewModel.Name, usuarioViewModel.Password);

            _usuarioRepository.Add(usuario);

            return Ok(new { message = "Usuário cadastrado com sucesso!" });
        }






        [Authorize]
        [HttpGet("validar")]
        public IActionResult Get()
        {
            var usuario = _usuarioRepository.Get();

            return Ok(usuario);

       
       

        }



        [HttpDelete]
        public async Task<ActionResult<UsuarioModel>> Delete(int id)
        {
            bool delatado = await _usuarioRepository.Delete(id);
            return Ok(delatado);

        }



    }

}


