using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Veiculos.Web.Model;
using Veiculos.Web.Services;

namespace Veiculos.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarroController : ControllerBase
    {
        private readonly ICarroService _veiculoService;
        public CarroController(ICarroService carroService)
        {
            _veiculoService = carroService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCarros()
        {
            var carros = await _veiculoService.GetAllCarros();
            return Ok(carros);
        }

        [HttpPost]
        public async Task<IActionResult> AddCarro([FromBody] CarroAddOrUpdate carroFromBody)
        {
            var carro = await _veiculoService.AddCarro(carroFromBody);

            if (carro == null)
            {
                return BadRequest();
            }

            return Ok(new
            {
                message = "Carro Cadastrado!",
                id = carro!.Id
            });
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateCarro([FromRoute] int id, [FromBody] Carro carroFromBody)
        {
            var carro = await _veiculoService.UpdateCarro(id, carroFromBody);
            if (carro == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                message = "Carro atualizado!",
                id = carro!.Id
            });
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!await _veiculoService.DeleteCarroByID(id))
            {
                return NotFound();
            }

            return Ok(new
            {
                message = "Carro deletado!",
                id = id
            });
        }
    }
}
