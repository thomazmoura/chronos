using System;
using System.Collections;
using System.Collections.Generic;

namespace Chronos.API.Entidades
{
    public class Contrato
    {
        public Guid Id { get; set; }
        public decimal ValorDaHora { get; set; }
        public ICollection<Folha> Folhas { get; set; }
    }
}