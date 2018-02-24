using System;
using System.Collections.Generic;

namespace Chronos.API.Entidades
{
    public class Folha
    {
        public Guid Id { get; set; }
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }
        public int QuantidadePrevistaDeDiasÚteis { get; set; }

        public Guid ContratoId { get; set; }
        public ICollection<Período> Períodos { get; set; }
    }
}