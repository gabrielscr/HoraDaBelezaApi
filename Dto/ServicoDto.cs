using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoraDaBelezaApi.Dto
{
    public class ServicoDto
    {
        public int Id { get; set; }

        public string Descricao { get; set; }

        public int Duracao { get; set; }

        public double Valor { get; set; }
    }
}
