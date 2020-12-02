namespace HoraDaBelezaApi.Dominio
{
    public class Token
    {
        public bool Autenticado { get; set; }

        public string CriadoEm { get; set; }

        public string ExpiraEm { get; set; }

        public string BearerToken { get; set; }

        public string Mensagem { get; set; }
    }
}
