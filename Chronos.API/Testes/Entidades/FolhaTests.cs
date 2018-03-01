using System;
using Chronos.API.Entidades;
using FluentAssertions;
using Xunit;

namespace Chronos.API.Testes.Entidades
{
    public class FolhaTests : EntidadeTests<Folha>
    {
        private readonly DateTime _dataQualquer = new DateTime(2015, 04, 01);
        private readonly Guid _guidQualquer = new Guid("2c94aa8d-c684-4dcb-9d2f-eb2372ac20c7");

        [Fact]
        public void PossuiTodosOsCamposObrigatorios_RetornaTrue_QuandoTodosOsDadosNecessáriosSãoInformados()
        {
            var contratoId = _guidQualquer;

            var folhaComDataInicialExplicita = new Folha()
            {
                DataInicial = _dataQualquer,
                ContratoId = contratoId
            };
            var folhaComDataInicialImplicita = new Folha(_dataQualquer.Month, _dataQualquer.Year)
            {
                ContratoId = contratoId
            };

            folhaComDataInicialExplicita.PossuiTodosOsCamposObrigatorios.Should().BeTrue();
            folhaComDataInicialImplicita.PossuiTodosOsCamposObrigatorios.Should().BeTrue();
        }

        [Fact]
        public void PossuiTodosOsCamposObrigatorios_RetornaFalse_QuandoADataInicialNãoÉInformadaNemGerada()
        {
            var folha = new Folha();

            folha.PossuiTodosOsCamposObrigatorios.Should().BeFalse();
        }

        [Theory]
        [InlineData(01, 02, 2018)] //Dia primeiro na semana
        [InlineData(02, 04, 2018)] //Dia primeiro no domingo
        [InlineData(03, 09, 2018)] //Dia primeiro no sábado
        public void ConstruirFolhaComMêsEAno_GeraDataInicialNoPrimeiroDiaDeSemana(int primeiroDiaUtilEsperado, int mes, int ano)
        {
            var dataInicialEsperada = new DateTime(ano, mes, primeiroDiaUtilEsperado);

            var folha = new Folha(mes, ano);

            folha.DataInicial.Should().Be(dataInicialEsperada);
        }
    }
}