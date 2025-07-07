using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ValidityControl.Application;
using System.Threading;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;
using ValidityControl.Application.ViewModel;
using ValidityControl.Application.ViewModel;
using ValidityControl.DoMain.Models;
using ValidityControl.DoMain;

namespace ValidityControl.Controllers.v1
{
    [ApiController]
    [Route("api/{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]

    public class ProductControlController : ControllerBase
    {
        private readonly IProductControlRepository _productcontrolrepository;
        private readonly ILogger<ProductControlController> _logger;



        public ProductControlController(IProductControlRepository productcontrolrepository, ILogger<ProductControlController> logger)
        {
            _productcontrolrepository = productcontrolrepository ?? throw new ArgumentNullException(nameof(productcontrolrepository));
            _logger = logger;
        }


         [HttpPost]
         public IActionResult Post([FromBody] ProductControlCreateViewModel viewModel)
         {
            //check functionality
            /*Console.WriteLine("Recebido no backend:");
            Console.WriteLine($"EAN: {viewModel.eanOfProduct}");
            Console.WriteLine($"Nome: {viewModel.nameOfProduct}");
            Console.WriteLine($"Validade: {viewModel.validity}");
            Console.WriteLine($"Descrição: {viewModel.description}");
            */

            if (!ModelState.IsValid)
            {
                var erros = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                Console.WriteLine("Erros de validação:");
                erros.ForEach(e => Console.WriteLine($"- {e}"));

                return BadRequest(new { Erros = erros });
            }
            var produtoExistente = _productcontrolrepository
            .GetProductsToday()
            .FirstOrDefault(p => p.ean == viewModel.eanOfProduct);

            var entity = new ProductControl(viewModel.eanOfProduct, viewModel.nameOfProduct, viewModel.validity, viewModel.description)
             {
                 ean = viewModel.eanOfProduct,
                 name = viewModel.nameOfProduct,
                 Validity = viewModel.validity,
                 description = viewModel.description
            };

             _productcontrolrepository.Add(entity);


             return Ok();
         }


        [HttpGet("products-today")]
        public IEnumerable<ProductControlViewModel> GetAll()
        {
            var hoje = DateTime.UtcNow.Date;

            return _productcontrolrepository.GetProductsToday()
                .Where(p => p.Validity >= hoje)
                .OrderBy(p => p.Validity)       
                .Select(p => new ProductControlViewModel(p))
                .ToList();
        }

        [HttpDelete]
        public async Task<ActionResult<ProductControl>> Delete(string ean)
        {
            bool delatado = await _productcontrolrepository.Delete(ean);
            return Ok(delatado);

        }
        [HttpDelete("Remove-products")]
        public IActionResult RemoveProduct()
        {
            _productcontrolrepository.RemoveProduct();
           
            return Ok("Product removed successfully");

        }

    }


}

