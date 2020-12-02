namespace HoraDaBelezaApi.Dto
{
    public class VendaDto
    {
        public int Id { get; set; }

        public string DataHora { get; set; }

        public string DataAgendada { get; set; }

        public double Valor { get; set; }

        public UsuarioDto Comprador { get; set; }

        public int ServicoId { get; set; }

        public ServicoDto Servico { get; set; }     
    }
}
