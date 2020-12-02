using HoraDaBelezaApi.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HoraDaBelezaApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SalaoController : ControllerBase
    {
        private readonly SalaoService salaoService;

        public SalaoController(SalaoService salaoService)
        {
            this.salaoService = salaoService;
        }

        [HttpGet]
        [Route("Obter")]
        public async Task<IActionResult> Obter([FromQuery] int id)
        {
            var json = await salaoService.Obter(id);

            return new JsonResult(json);
        }

        [HttpGet]
        [Route("Listar")]
        public async Task<IActionResult> Listar()
        {
            var json = await salaoService.Listar();

            return new JsonResult(json);
        }
    }
}
