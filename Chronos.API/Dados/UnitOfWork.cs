using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chronos.API.Dados;

namespace Chronos.API.Dados
{
    public class UnitOfWork : IUnitOfWork
    {
        public Queue<Action> AcoesPrevias { get; private set; }

        public Queue<Action> AcoesPosteriores { get; private set; }

        private readonly ContextoDeDadosChronos _context;
        public UnitOfWork(ContextoDeDadosChronos context)
        {
            _context = context;

            AcoesPrevias = new Queue<Action>();
            AcoesPosteriores = new Queue<Action>();
        }

        public async Task SalvarAlteracoes()
        {
            while (AcoesPrevias.Any())
                AcoesPrevias.Dequeue().Invoke();

            await _context.SaveChangesAsync();

            while (AcoesPosteriores.Any())
                AcoesPosteriores.Dequeue().Invoke();
        }
    }
}