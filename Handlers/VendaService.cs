using HoraDaBelezaApi.Dominio;
using HoraDaBelezaApi.Dto;
using HoraDaBelezaApi.Infra.Contexto;
using HoraDaBelezaApi.Infra.Servicos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HoraDaBelezaApi.Handlers
{
    public class VendaService
    {
        private readonly ServerContext _serverContext;
        private readonly IAuthService _authService;

        public VendaService(ServerContext serverContext, IAuthService authService)
        {
            _serverContext = serverContext;
            _authService = authService;
        }

        public async Task<VendaDto[]> Listar()
        {
            var usuarioId = await _authService.ObterUsuarioId();

            return await _serverContext
                 .Set<Venda>()
                 .AsNoTracking()
                 .Where(w => w.CompradorId == usuarioId)
                 .Select(e => new VendaDto
                 {
                     Id = e.Id,
                     Valor = e.Valor,
                     DataHora = e.DataHora.ToString("dd/MM/yyyy HH:mm"),
                     DataAgendada = e.DataAgendada.ToString("dd/MM/yyyy HH:mm"),
                 })
                 .ToArrayAsync();
        }

        public async Task<VendaDto> Obter(int pedidoId)
        {
            return await _serverContext
                    .Set<Venda>()
                    .AsNoTracking()
                    .Where(e => e.Id == pedidoId)
                    .Select(e => new VendaDto
                    {
                        Id = e.Id,
                        DataHora = e.DataHora.ToString("dd/MM/yyyy HH:mm"),
                        DataAgendada = e.DataAgendada.ToString("dd/MM/yyyy HH:mm"),
                        Valor = e.Valor,
                        Comprador = new UsuarioDto
                        {
                            Nome = e.Comprador.Nome,
                            Email = e.Comprador.Email,
                            Id = e.Comprador.Id
                        },
                        Servico = new ServicoDto
                        {
                            Descricao = e.Servico.Descricao,
                            Duracao = e.Servico.Duracao,
                            Id = e.Servico.Id,
                            Valor = e.Servico.Valor
                        }
                    })
                    .FirstOrDefaultAsync();
        }

        public async Task<Resultado> Inserir(VendaDto request)
        {
            var usuarioId = await _authService.ObterUsuarioId();

            var venda = new Venda
            {
                CompradorId = usuarioId,
                DataHora = DateTimeOffset.Now,
                DataAgendada = DateTimeOffset.Parse(request.DataAgendada),
                Valor = request.Valor,
                ServicoId = request.ServicoId
            };

            await _serverContext.AddAsync(venda);

            await _serverContext.SaveChangesAsync();

            return new Resultado
            {
                Erro = null,
                Sucesso = true,
                Mensagem = "Sua compra foi realizada com sucesso, para mais detalhes verifique no menu de Meus Pedidos."
            };
        }
    }
}
