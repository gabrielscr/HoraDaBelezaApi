using HoraDaBelezaApi.Dominio;
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
        [AllowAnonymous]
        [HttpPost]
        [Route("Autenticar")]
        public async Task<object> Autenticar([FromBody] Usuario usuario, [FromServices] AccessManager accessManager)
        {
            var valido = await accessManager.ValidarCredenciais(usuario);

            if (valido)
            {
                return accessManager.GerarToken(usuario);
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
            }, usuario.Senha, Roles.USUARIO);
        }
    }
}
