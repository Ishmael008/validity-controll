
﻿using Microsoft.AspNetCore.Http;
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
﻿using Microsoft.AspNetCore.Authorization;



namespace ValidityControl.Controllers.v1
{
    [ApiController]
    [Route("api/{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]

    public class ProductControlController : ControllerBase
    {
        private readonly IProductControlRepository _productcontrolrepository;
        private readonly IProductFeedbackRepository _productfeedbackrepository;
        private readonly ILogger<ProductControlController> _logger;



        public ProductControlController(IProductControlRepository productcontrolrepository, ILogger<ProductControlController> logger, IProductFeedbackRepository productfeedbackrepository)
        {
            _productcontrolrepository = productcontrolrepository ?? throw new ArgumentNullException(nameof(productcontrolrepository));
            _logger = logger;
            _productfeedbackrepository = productfeedbackrepository;
        }





        [HttpPost]
        public async Task<ActionResult<ProductControl>> Post([FromBody] ProductControlCreateViewModel viewModel)
        {
            //check functionality
            /*Console.WriteLine("Recebido no backend:");
            Console.WriteLine($"EAN: {viewModel.eanOfProduct}");
            Console.WriteLine($"Nome: {viewModel.nameOfProduct}");
            Console.WriteLine($"Validade: {viewModel.validity}");
            Console.WriteLine($"Descrição: {viewModel.description}");
            */
            try
            {
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
                .GetProducts()
                .FirstOrDefault(p => p.ean == viewModel.eanOfProduct);

                var entity = new ProductControl(viewModel.eanOfProduct, viewModel.nameOfProduct, viewModel.validity, viewModel.description)
                {
                    ean = viewModel.eanOfProduct,
                    name = viewModel.nameOfProduct,
                    Validity = viewModel.validity,
                    description = viewModel.description
                };


                await _productcontrolrepository.Add(entity);

                return Ok();
            }catch (Exception ex)
            {
                return StatusCode(500, $"erro interno:{ex.Message}");
            }
        }

        [HttpPost("feedback")]
        public async Task<IActionResult> PostFeedback([FromBody] ProductFeedbackViewModel feedback)
        {

            var productFeedback = new ProductFeedback(feedback.EanOfProduct, feedback.QuestionOfProduct1, feedback.QuestionOfProduct2, feedback.CreatedAtProduct)
            {
                Ean = feedback.EanOfProduct,
                Question1 = feedback.QuestionOfProduct1,
                Question2 = feedback.QuestionOfProduct2,
                CreatedAt = feedback.CreatedAtProduct
            };

            await _productfeedbackrepository.Add(productFeedback);
            return Ok(new { message = "Feedback enviado com sucesso." });
        }





        [HttpGet("products")]
        public  IEnumerable<ProductControlViewModel> GetAll()
        {
            var today = DateTime.UtcNow.Date;

            return _productcontrolrepository.GetProducts()
                .Where(p => p.Validity >= today)

                .OrderBy(p => p.Validity)       

             

                .Select(p => new ProductControlViewModel(p))
                .ToList();
        }

        [HttpPut("{ean}")]
        public async Task<ActionResult<ProductControl>> UpdateProduct(ProductControl product, string ean)
        {
            product.ean = ean;
          var upadate =  _productcontrolrepository.Update(product, ean);
            return Ok(upadate);

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

