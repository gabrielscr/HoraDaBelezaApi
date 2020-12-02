namespace HoraDaBelezaApi.Dominio
{
    public class TokenConfiguracao
    {
        public string Audience { get; set; }

        public string Issuer { get; set; }

        public int Seconds { get; set; }
    }
}
