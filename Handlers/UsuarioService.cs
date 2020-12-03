using HoraDaBelezaApi.Dominio;
using HoraDaBelezaApi.Dto;
using HoraDaBelezaApi.Infra.Contexto;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HoraDaBelezaApi.Handlers
{
    public class UsuarioService
    {
        private readonly ServerContext _serverContext;

        public UsuarioService(ServerContext serverContext)
        {
            _serverContext = serverContext;
        }

        public async Task Editar(UsuarioDto request)
        {
            var model = await ObterParaEditar(request.Id);

            Mapear(model, request);

            await _serverContext.SaveChangesAsync();
        }

        private async Task<Usuario> ObterParaEditar(string usuarioId)
        {
            return await _serverContext
                .Set<Usuario>()
                .FirstOrDefaultAsync(f => f.Id == usuarioId);
        }

        private void Mapear(Usuario usuario, UsuarioDto request)
        {
            usuario.Nome = request.Nome;
        }

        public async Task<UsuarioDto> Obter(string id)
        {
            var usuario = await _serverContext
                .Set<Usuario>()
                .Where(w => w.Id == id)
                .Select(s => new UsuarioDto
                {
                    Email = s.Email,
                    Nome = s.Nome,
                    Id = s.Id
                })
                .FirstOrDefaultAsync();

            return usuario;
        }

        public async Task<UsuarioDto> ObterComEmail(string email)
        {
            var usuario = await _serverContext
                .Set<Usuario>()
                .Where(w => w.Email == email)
                .Select(s => new UsuarioDto
                {
                    Email = s.Email,
                    Nome = s.Nome,
                    Id = s.Id
                })
                .FirstOrDefaultAsync();

            return usuario;
        }

        public async Task Inserir(Usuario usuario)
        {
            await _serverContext.AddAsync(usuario);

            await _serverContext.SaveChangesAsync();
        }
    }
}
