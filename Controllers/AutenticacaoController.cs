using HoraDaBelezaApi.Dominio;
using HoraDaBelezaApi.Handlers;
using HoraDaBelezaApi.Infra.Seguranca;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HoraDaBelezaApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AutenticacaoController : ControllerBase
    {

        private readonly UsuarioService _usuarioService;

        public AutenticacaoController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Autenticar")]
        public async Task<object> Autenticar([FromBody] Usuario usuario, [FromServices] AccessManager accessManager)
        {
            var valido = await accessManager.ValidarCredenciais(usuario);

            if (valido)
            {
                return new
                {
                    Token = accessManager.GerarToken(usuario),
                    Usuario = await _usuarioService.ObterComEmail(usuario.Email)
                };
            }

            else
            {
                return new
                {
                    Autenticado = false,
                    Mensagem = "Falha ao autenticar"
                };
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Cadastrar")]
        public async Task<Resultado> Cadastrar([FromBody] Usuario usuario, [FromServices] AccessManager accessManager)
        {
            return await accessManager.CriarUsuario(new ApplicationUser()
            {
                UserName = usuario.Email,
                Email = usuario.Email,
                EmailConfirmed = true
            }, usuario, Roles.USUARIO);
        }
    }
}
