using HoraDaBelezaApi.Dominio;
using HoraDaBelezaApi.Infra.Contexto;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoraDaBelezaApi.Infra.Seguranca
{
    public class IdentityInitializer
    {
        private readonly IdentityContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentityInitializer(
            IdentityContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            if (!_roleManager.RoleExistsAsync(Roles.USUARIO).Result)
            {
                var resultado = _roleManager.CreateAsync(
                    new IdentityRole(Roles.USUARIO)).Result;
                if (!resultado.Succeeded)
                {
                    throw new Exception(
                        $"Erro durante a criação da role {Roles.USUARIO}.");
                }
            }
        }
    }
}
