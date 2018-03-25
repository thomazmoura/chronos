using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chronos.API.Dados
{
    public interface IUnitOfWork
    {
        Queue<Action> AcoesPrevias { get; }
        Queue<Action> AcoesPosteriores { get; }
        Task SalvarAlteracoes();
    }
}