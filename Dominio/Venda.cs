using System;

namespace HoraDaBelezaApi.Dominio
{
    public class Venda
    {
        public int Id { get; set; }

        public string CompradorId { get; set; }

        public Usuario Comprador { get; set; }

        public int ServicoId { get; set; }

        public Servico Servico { get; set; }

        public DateTimeOffset DataHora { get; set; }

        public DateTimeOffset DataAgendada { get; set; }

        public double Valor { get; set; }
    }
}
