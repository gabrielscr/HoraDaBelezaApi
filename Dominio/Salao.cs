using System.Collections.Generic;

namespace HoraDaBelezaApi.Dominio
{
    public class Salao
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Profissional { get; set; }

        public string Imagem { get; set; }

        public List<SalaoServico> Servicos { get; set; }
    }
}
