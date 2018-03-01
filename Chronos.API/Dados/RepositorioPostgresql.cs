using System;
using System.Linq;
using Chronos.API.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Chronos.API.Dados
{
    public class RepositorioPostgresql : IRepositorio
    {
        public IQueryable<Contrato> Contratos => _contexto.Contratos;
        public IQueryable<Folha> Folhas => _contexto.Folhas;
        public IQueryable<Periodo> Periodos => _contexto.Periodos;

        private readonly ContextoDeDadosChronos _contexto;

        public RepositorioPostgresql(ContextoDeDadosChronos contexto)
        {
            _contexto = contexto;
        }

        public void Acrescentar<T>(T entidade) where T : Entidade
        {
            _contexto.Set<T>().Add(entidade);
        }

        public void Atualizar<T>(T entidade) where T : Entidade
        {
            _contexto.Set<T>().Update(entidade);
            _contexto.Entry(entidade).State = EntityState.Modified;
        }

        public void Remover<T>(T entidade) where T : Entidade
        {
            _contexto.Set<T>().Remove(entidade);
        }

        public void Remover<T>(Guid id) where T : Entidade, new()
        {
            var entidade = new T()
            {
                Id = id
            };
            _contexto.Set<T>().Attach(entidade);
            _contexto.Set<T>().Remove(entidade);
        }
    }
}