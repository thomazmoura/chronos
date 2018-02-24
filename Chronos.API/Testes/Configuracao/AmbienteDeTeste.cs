using System;
using Chronos.API.Dados;
using Microsoft.EntityFrameworkCore;

namespace Chronos.API.Testes.Configuracao
{
    public class AmbienteDeTeste
    {
        public ContextoDeDadosChronos Contexto { get; set; }
        public IUnitOfWork UnitOfWork { get; set; }
        public IRepositorio Repositorio { get; set; }

        public static AmbienteDeTeste NovoAmbiente(string chaveDoBanco = null)
        {
            chaveDoBanco = chaveDoBanco ?? Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ContextoDeDadosChronos>()
                .UseInMemoryDatabase(chaveDoBanco)
                .EnableSensitiveDataLogging()
                .Options;
            var contexto = new ContextoDeDadosChronos(options);

            return new AmbienteDeTeste()
            {
                Contexto = contexto,
                UnitOfWork = new UnitOfWork(contexto),
                Repositorio = new RepositorioPostgresql(contexto)
            };
        }
    }
}