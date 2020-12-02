using HoraDaBelezaApi.Dto;
using HoraDaBelezaApi.Handlers;
using HoraDaBelezaApi.Infra.Servicos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoraDaBelezaApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _service;
        private readonly IAuthService _authService;

        public UsuarioController(UsuarioService service, IAuthService authService)
        {
            _service = service;
            _authService = authService;
        }

        [HttpPut]
        [Route("Editar")]
        public async Task Editar([FromBody] UsuarioDto request)
        {
            await _service.Editar(request);
        }

        [HttpGet]
        [Route("Obter")]
        public async Task<IActionResult> Obter()
        {
            var usuario = await _authService.ObterUsuario();

            return new JsonResult(usuario);
        }
    }
}
