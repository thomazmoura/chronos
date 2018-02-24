using System;

namespace Chronos.API.Entidades
{
    public class Periodo : Entidade<Periodo>
    {
        public DateTime HorarioDeInicio { get; set; }
        public DateTime HorarioDeEncerramento { get; set; }
        public string Descricao { get; set; }

        public Guid FolhaId { get; set; }

        internal override bool PossuiTodosOsCamposObrigatorios =>
            !String.IsNullOrWhiteSpace(Descricao);
    }
}