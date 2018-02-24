using System;
using System.Collections.Generic;

namespace Chronos.API.Entidades
{
    public class Folha : Entidade<Folha>
    {
        public DateTime DataInicial { get; set; }
        public DateTime? DataFinal { get; set; }
        public int QuantidadePrevistaDeDiasÚteis { get; set; }

        public Guid ContratoId { get; set; }
        public ICollection<Periodo> Periodos { get; set; }

        public Folha() { }
        public Folha(int mes, int ano)
        {
            DefinirDataAtual(mes, ano);
        }

        internal override bool PossuiTodosOsCamposObrigatorios =>
            DataInicial != default(DateTime) &&
            ContratoId != default(Guid);

        private void DefinirDataAtual(int mes, int ano)
        {
            var possivelPrimeiroDiaDaSemana = new DateTime(ano, mes, 1);
            while (!EDiaDaSemana(possivelPrimeiroDiaDaSemana))
                possivelPrimeiroDiaDaSemana = possivelPrimeiroDiaDaSemana.AddDays(1);
            DataInicial = possivelPrimeiroDiaDaSemana;
        }

        private bool EDiaDaSemana(DateTime data)
        {
            return data.DayOfWeek != DayOfWeek.Saturday && data.DayOfWeek != DayOfWeek.Sunday;
        }
    }
}