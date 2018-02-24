using Chronos.API.Entidades;
using FluentAssertions;
using Xunit;

namespace Chronos.API.Testes.Entidades
{
    public class ContratoTests : EntidadeTests<Contrato>
    {
        [Fact]
        public void PossuiTodosOsCamposObrigatorios_RetornaFalse_QuandoOValorDaHoraNãoFoiInformado()
        {
            var contrato = new Contrato();
            contrato.PossuiTodosOsCamposObrigatorios.Should().BeFalse();
        }
    }
}