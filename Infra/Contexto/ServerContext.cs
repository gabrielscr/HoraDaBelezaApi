using HoraDaBelezaApi.Dominio;
using Microsoft.EntityFrameworkCore;

namespace HoraDaBelezaApi.Infra.Contexto
{
    public class ServerContext : DbContext
    {
        public ServerContext(DbContextOptions<ServerContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>();

            modelBuilder.Entity<Venda>(opts =>
            {
                opts.HasOne(e => e.Comprador)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

                opts.HasOne(e => e.Servico)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Servico>();

            modelBuilder.Entity<Salao>();

            modelBuilder.Entity<SalaoServico>();
        }
    }
}
