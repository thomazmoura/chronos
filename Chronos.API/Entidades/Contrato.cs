using System;
using System.Collections;
using System.Collections.Generic;

namespace Chronos.API.Entidades
{
    public class Contrato : Entidade<Contrato>
    {
        public decimal ValorDaHora { get; set; }
        public ICollection<Folha> Folhas { get; set; }

        internal override bool PossuiTodosOsCamposObrigatorios =>
            ValorDaHora > 0;
    }
}