using HoraDaBelezaApi.Dominio;
using HoraDaBelezaApi.Dto;
using HoraDaBelezaApi.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace HoraDaBelezaApi.Infra.Servicos
{
    public class AuthService : IAuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UsuarioService _service;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthService(IHttpContextAccessor httpContextAccessor, UsuarioService service, UserManager<ApplicationUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _service = service;
            _userManager = userManager;
        }

        public async Task<UsuarioDto> ObterUsuario()
        {
            var token = _httpContextAccessor?
                .HttpContext?
                .Request?
                .Headers["Authorization"];

            UsuarioDto usuario = new UsuarioDto();

            if (!string.IsNullOrEmpty(token))
            {
                var tokenDecoder = new JwtSecurityTokenHandler();

                var jwtSecurityToken = tokenDecoder.ReadJwtToken(token);

                var email = jwtSecurityToken.Claims.Where(w => w.Type == "EMAIL").FirstOrDefault().ToString();

                if (!string.IsNullOrEmpty(email))
                {
                    email = email.Replace("email: ", "");

                    var user = await _userManager.FindByEmailAsync(email);

                    var model = await _service.Obter(user?.Id);

                    usuario = model;
                }
            }

            return usuario;
        }

        public async Task<string> ObterUsuarioId()
        {
            var token = _httpContextAccessor?
                .HttpContext?
                .Request?
                .Headers["Authorization"];

            var usuarioId = "";

            if (!string.IsNullOrEmpty(token))
            {
                var tokenDecoder = new JwtSecurityTokenHandler();

                var newToken = token.ToString().Replace("Bearer ", "");

                var jwtSecurityToken = tokenDecoder.ReadJwtToken(newToken);

                var email = jwtSecurityToken.Claims.Where(w => w.Type == "EMAIL").FirstOrDefault().ToString();

                if (!string.IsNullOrEmpty(email))
                {
                    email = email.Replace("EMAIL: ", "");

                    var user = await _userManager.FindByEmailAsync(email);

                    var model = await _service.Obter(user?.Id);

                    usuarioId = model.Id;
                }
            }

            return usuarioId;
        }
    }
}
