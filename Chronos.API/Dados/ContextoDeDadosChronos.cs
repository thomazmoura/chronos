using Chronos.API.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Chronos.API.Dados
{
    public class ContextoDeDadosChronos : DbContext
    {
        public ContextoDeDadosChronos(DbContextOptions<ContextoDeDadosChronos> options) : base(options) { }

        public DbSet<Contrato> Contratos { get; set; }
        public DbSet<Folha> Folhas { get; set; }
        public DbSet<Periodo> Periodos { get; set; }
    }
}