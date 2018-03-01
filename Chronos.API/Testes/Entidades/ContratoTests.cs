using Chronos.API.Entidades;
using FluentAssertions;
using Xunit;

namespace Chronos.API.Testes.Entidades
{
    public class ContratoTests : EntidadeTests<Contrato>
    {
        [Fact]
        public void PossuiTodosOsCamposObrigatorios_RetornaTrue_QuandoTodosOsCamposNecessáriosEstãoPreenchidosComValoresVálidos()
        {
            var contrato = new Contrato()
            {
                ValorDaHora = 50
            };
            contrato.PossuiTodosOsCamposObrigatorios.Should().BeTrue();
        }

        [Fact]
        public void PossuiTodosOsCamposObrigatorios_RetornaFalse_QuandoOValorDaHoraNãoFoiInformado()
        {
            var contrato = new Contrato();
            contrato.PossuiTodosOsCamposObrigatorios.Should().BeFalse();
        }

        [Fact]
        public void PossuiTodosOsCamposObrigatorios_RetornaFalse_QuandoOValorDaHoraÉNegativo()
        {
            var contrato = new Contrato()
            {
                ValorDaHora = -400
            };
            contrato.PossuiTodosOsCamposObrigatorios.Should().BeFalse();
        }

        [Fact]
        public void PossuiTodosOsCamposObrigatorios_RetornaFalse_QuandoOValorDaHoraÉZero()
        {
            var contrato = new Contrato()
            {
                ValorDaHora = 0
            };
            contrato.PossuiTodosOsCamposObrigatorios.Should().BeFalse();
        }
    }
}