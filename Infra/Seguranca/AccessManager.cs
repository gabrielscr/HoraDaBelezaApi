using HoraDaBelezaApi.Dominio;
using HoraDaBelezaApi.Handlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace HoraDaBelezaApi.Infra.Seguranca
{
    public class AccessManager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly SigningConfigurations _signingConfigurations;
        private readonly TokenConfiguracao _tokenConfigurations;
        private readonly UsuarioService _usuarioService;

        public AccessManager(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            SigningConfigurations signingConfigurations,
            TokenConfiguracao tokenConfigurations,
            UsuarioService usuarioService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
            _usuarioService = usuarioService;
        }

        public async Task<bool> ValidarCredenciais(Usuario user)
        {
            bool credenciaisValidas = false;
            if (user != null && !string.IsNullOrWhiteSpace(user.Email))
            {
                var userIdentity = await _userManager
                    .FindByEmailAsync(user.Email);

                credenciaisValidas = await VerificarSeCredenciaisValidas(user, credenciaisValidas, userIdentity);
            }

            return credenciaisValidas;
        }

        private async Task<bool> VerificarSeCredenciaisValidas(Usuario user, bool credenciaisValidas, ApplicationUser userIdentity)
        {
            if (userIdentity != null)
            {
                var resultadoLogin = await _signInManager
                    .CheckPasswordSignInAsync(userIdentity, user.Senha, false);

                if (resultadoLogin.Succeeded)
                {
                    credenciaisValidas = await _userManager.IsInRoleAsync(
                        userIdentity, Roles.USUARIO);
                }
            }

            return credenciaisValidas;
        }

        public async Task<Resultado> CriarUsuario(ApplicationUser user, string password, string initialRole = null)
        {
            var resultado = new IdentityResult();

            var usuario = await _userManager.FindByNameAsync(user.UserName);

            usuario = await _userManager.FindByEmailAsync(user.Email);

            if (usuario == null)
            {
                resultado = await _userManager
                    .CreateAsync(user, password);

                if (resultado.Succeeded &&
                    !string.IsNullOrWhiteSpace(initialRole))
                {
                    await _userManager.AddToRoleAsync(user, initialRole);

                    await _usuarioService.Inserir(new Usuario
                    {
                        Id = user.Id,
                        Senha = user.PasswordHash,
                        Email = user.Email
                    });
                }

                return new Resultado
                {
                    Erro = null,
                    Sucesso = resultado.Succeeded
                };
            }


            return new Resultado
            {
                Erro = $"Usuário ou e-mail já existente, tente outro.",
                Sucesso = false
            };
        }

        public Token GerarToken(Usuario user)
        {
            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(user.Email, "EMAIL"),
                new[] {
                        new Claim("EMAIL", user.Email),
                }
            ); ;

            DateTime dataCriacao = DateTime.Now;
            DateTime dataExpiracao = dataCriacao +
                TimeSpan.FromSeconds(_tokenConfigurations.Seconds);

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfigurations.Issuer,
                Audience = _tokenConfigurations.Audience,
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = dataCriacao,
                Expires = dataExpiracao
            });
            var token = handler.WriteToken(securityToken);

            return new Token()
            {
                Autenticado = true,
                CriadoEm = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                ExpiraEm = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                BearerToken = token,
                Mensagem = "Logado com sucesso."
            };
        }
    }
}
