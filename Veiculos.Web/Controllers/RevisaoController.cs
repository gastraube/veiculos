using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Veiculos.Domain.Model;
using Veiculos.Domain.Models;
using Veiculos.Service.Services;
using Veiculos.Service.Services.Abstraction;

namespace Veiculos.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RevisaoController : ControllerBase
    {
        private readonly IRevisaoService _revisaoService;
        public RevisaoController(IRevisaoService revisaoService)
        {
            _revisaoService = revisaoService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRevisoes([FromBody] List<RevisaoDTO> revisoesFromBody, [FromQuery] int veiculoId)
        {
            var revisoes = await _revisaoService.CreateRevisoes(revisoesFromBody, veiculoId);
            if (revisoes == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                message = "Revisões criadas!"
            });
        }


        [HttpGet]
        [EnableCors("default")]
        public async Task<IActionResult> GetRevisoesByVeiculoId(int VeiculoId)
        {
            var revisoes = await _revisaoService.GetRevisoesByVeiculoId(VeiculoId);

            if (revisoes == null)
            {
                return NotFound();
            }


            return Ok(revisoes);
        }
    }
}
