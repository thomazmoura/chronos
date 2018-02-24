using System;
using System.Linq;
using Chronos.API.Entidades;

namespace Chronos.API.Dados
{
    public interface IRepositorio
    {
        IQueryable<Contrato> Contratos { get; }
        IQueryable<Folha> Folhas { get; }
        IQueryable<Período> Períodos { get; }

        void Acrescentar<TEntidade>(TEntidade entidade) where TEntidade : Entidade;
        void Atualizar<TEntidade>(TEntidade entidade) where TEntidade : Entidade;
        void Remover<TEntidade>(TEntidade entidade) where TEntidade : Entidade;
        void Remover<TEntidade>(Guid id) where TEntidade : Entidade, new();
    }
}