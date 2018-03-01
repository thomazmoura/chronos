using Chronos.API.Entidades;
using FluentAssertions;
using Xunit;

namespace Chronos.API.Testes.Entidades
{
    public class PeriodoTests : EntidadeTests<Periodo>
    {
        [Fact]
        public void PossuiTodosOsCamposObrigatorios_RetornaTrue_QuandoTodosOsDadosNecessáriosSãoInformados()
        {
            var periodo = new Periodo()
            {
                Descricao = "Realização de testes"
            };

            periodo.PossuiTodosOsCamposObrigatorios.Should().BeTrue();
        }


        [Fact]
        public void PossuiTodosOsCamposObrigatorios_RetornaFalse_QuandoADescricaoNãoÉInformada()
        {
            var periodo = new Periodo()
            {
                Descricao = " "
            };

            periodo.PossuiTodosOsCamposObrigatorios.Should().BeFalse();
        }
    }
}