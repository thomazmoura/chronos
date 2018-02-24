using System;

namespace Chronos.API.Entidades
{
    public class Período
    {
        public Guid Id { get; set; }
        public DateTime HorárioDeInício { get; set; }
        public DateTime HorárioDeEncerramento { get; set; }
        public string Descrição { get; set; }

        public Guid FolhaId { get; set; }
    }
}