using HoraDaBelezaApi.Dominio;
using HoraDaBelezaApi.Dto;
using HoraDaBelezaApi.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HoraDaBelezaApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VendaController : ControllerBase
    {
        private readonly VendaService vendaService;

        public VendaController(VendaService vendaService)
        {
            this.vendaService = vendaService;
        }

        [HttpGet]
        [Route("Listar")]
        public async Task<IActionResult> Listar()
        {
            var json = await vendaService.Listar();

            return new JsonResult(json);
        }

        [HttpGet]
        [Route("Obter")]
        public async Task<IActionResult> Obter([FromQuery] int id)
        {
            var json = await vendaService.Obter(id);

            return new JsonResult(json);
        }

        [HttpPost]
        [Route("Inserir")]
        public async Task<Resultado> Inserir([FromBody] VendaDto request)
        {
            var resultado = await vendaService.Inserir(request);

            return resultado;
        }
    }
}
