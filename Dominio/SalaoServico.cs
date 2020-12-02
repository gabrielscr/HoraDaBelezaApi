using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoraDaBelezaApi.Dominio
{
    public class SalaoServico
    {
        public int Id { get; set; }

        public int ServicoId { get; set; }

        public Servico Servico { get; set; }

        public int SalaoId { get; set; }

        public Salao Salao { get; set; }
    }
}
