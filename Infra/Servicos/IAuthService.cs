using HoraDaBelezaApi.Dto;
using System.Threading.Tasks;

namespace HoraDaBelezaApi.Infra.Servicos
{
    public interface IAuthService
    {
        Task<string> ObterUsuarioId();

        Task<UsuarioDto> ObterUsuario();
    }
}
