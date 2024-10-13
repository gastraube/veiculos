using Microsoft.AspNetCore.Mvc;
using Veiculos.Web.Model;
using Veiculos.Web.Services;

namespace Veiculos.Web.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CaminhaoController : ControllerBase
    {
        private readonly ICaminhaoService _caminhaoService;
        public CaminhaoController(ICaminhaoService heroService)
        {
            _caminhaoService = heroService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCaminhaos()
        {
            var heros = await _caminhaoService.GetAllCaminhoes();
            return Ok(heros);
        }

        [HttpPost]
        public async Task<IActionResult> AddCaminhao([FromBody] Caminhao Caminhao)
        {
            var hero = await _caminhaoService.AddCaminhao(Caminhao);

            if (hero == null)
            {
                return BadRequest();
            }

            return Ok(new
            {
                message = "Caminhao Cadastrado!",
                id = hero!.Id
            });
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateCaminhao([FromRoute] int id, [FromBody] Caminhao Caminhao)
        {
            var hero = await _caminhaoService.UpdateCaminhao(id, Caminhao);
            if (hero == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                message = "Caminhao atualizado!",
                id = hero!.Id
            });
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!await _caminhaoService.DeleteCaminhaoByID(id))
            {
                return NotFound();
            }

            return Ok(new
            {
                message = "Caminhao deletado!",
                id = id
            });
        }
    }
}
