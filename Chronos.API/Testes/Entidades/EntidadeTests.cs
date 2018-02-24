using FluentAssertions;
using Chronos.API.Entidades;
using System;
using Xunit;

namespace Chronos.API.Testes.Entidades
{
    public abstract class EntidadeTests<TEntidade> where TEntidade : Entidade<TEntidade>, new()
    {
        [Fact]
        public void EntidadeEquivaleA_RetornaFalse_QuandoIdsSãoDiferentes()
        {
            var entidadeComparada = new TEntidade()
            {
                Id = new Guid("496191e7-5e51-4453-ac36-8bada19c9ece")
            };
            var entidadeReferencia = new TEntidade()
            {
                Id = new Guid("baeebc6c-5f68-42e3-a5cb-37948961ad05")
            };

            var entidadesSaoEquivalentes = entidadeComparada.EquivaleA(entidadeReferencia);

            entidadesSaoEquivalentes.Should().BeFalse();
        }
    }
}