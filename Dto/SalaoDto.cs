using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoraDaBelezaApi.Dto
{
    public class SalaoDto
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Profissional { get; set; }

        public string Imagem { get; set; }

        public ServicoDto[] Servicos { get; set; }
    }
}
