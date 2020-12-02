using HoraDaBelezaApi.Dominio;
using HoraDaBelezaApi.Dto;
using HoraDaBelezaApi.Infra.Contexto;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HoraDaBelezaApi.Handlers
{
    public class SalaoService
    {
        private readonly ServerContext _serverContext;

        public SalaoService(ServerContext serverContext)
        {
            _serverContext = serverContext;
        }

        public async Task<SalaoDto> Obter(int id)
        {
            return await _serverContext
                .Set<Salao>()
                .AsNoTracking()
                .Where(w => w.Id == id)
                .Select(s => new SalaoDto
                {
                    Id = s.Id,
                    Imagem = s.Imagem,
                    Nome = s.Nome,
                    Profissional = s.Profissional,
                    Servicos = s.Servicos
                        .Select(x => new ServicoDto
                        {
                            Id = x.Servico.Id,
                            Descricao = x.Servico.Descricao,
                            Duracao = x.Servico.Duracao,
                            Valor = x.Servico.Valor
                        })
                    .ToArray()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<SalaoDto[]> Listar()
        {
            return await _serverContext
                .Set<Salao>()
                .AsNoTracking()
                .Select(s => new SalaoDto
                {
                    Id = s.Id,
                    Imagem = s.Imagem,
                    Nome = s.Nome,
                    Profissional = s.Profissional
                })
                .ToArrayAsync();
        }
    }
}
