using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Veiculos.Domain.Models;
using Veiculos.Service.Services.Abstraction;

namespace Veiculos.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeiculoController : ControllerBase
    {
        private readonly IVeiculoService _veiculoService;
        public VeiculoController(IVeiculoService heroService)
        {
            _veiculoService = heroService;
        }

        [HttpGet]
        [EnableCors("default")]
        public async Task<IActionResult> GetVeiculos()
        {
            var veiculos = await _veiculoService.GetAllVeiculos();
            return Ok(veiculos);
        }

        [HttpGet("/id")]
        [EnableCors("default")]
        public async Task<IActionResult> GetVeiculoById(int Id)
        {
            var veiculos = await _veiculoService.GetVeiculoById(Id);
            return Ok(veiculos);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVeiculo([FromBody] VeiculoDTO veiculoFromBody)
        {

            var veiculo = await _veiculoService.CreateVeiculo(veiculoFromBody);
            if (veiculo == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                message = "Veiculo atualizado!",
                id = veiculo!.Id
            });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateVeiculo([FromBody] VeiculoDTO veiculoFromBody)
        {

            var veiculo = await _veiculoService.UpdateVeiculo(veiculoFromBody);
            if (veiculo == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                message = "Veiculo atualizado!",
                id = veiculo!.Id
            });
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!await _veiculoService.DeleteVeiculoByID(id))
            {
                return NotFound();
            }

            return Ok(new
            {
                message = "Veiculo deletado!",
                id = id
            });
        }
    }
}
