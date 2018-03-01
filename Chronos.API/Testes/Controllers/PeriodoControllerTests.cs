using System;
using System.Collections.Generic;
using Chronos.API.Controllers;
using Chronos.API.Entidades;

namespace Chronos.API.Testes.Controllers
{
    public class PeriodoControllerTests : ApiControllerTests<Periodo>
    {
        private readonly DateTime DataQualquer = new DateTime(2018, 04, 01);

        private PeriodoController _periodoController;
        protected override IEntidadeApi<Periodo> ObterApiController()
        {
            _periodoController = new PeriodoController(_ambienteDeTeste.Repositorio);
            return _periodoController;
        }

        protected override Periodo ObterExemploEntidadeInvalidaParaAtualizacao()
        {
            return new Periodo();
        }

        protected override Periodo ObterExemploEntidadeInvalidaParaInsercao()
        {
            return new Periodo()
            {
                Id = new Guid("2b74047d-9b50-47d6-b4c9-a299136d9ef1")
            };
        }

        protected override IEnumerable<Periodo> ObterExemploEntidades()
        {
            return new[]
            {
                new Periodo()
                {
                    HorarioDeInicio = DataQualquer.AddHours(15),
                    HorarioDeEncerramento = DataQualquer.AddHours(18),
                    Descricao = "Criação de testes"
                },
                new Periodo()
                {
                    HorarioDeInicio = DataQualquer.AddDays(1).AddHours(15),
                    HorarioDeEncerramento = DataQualquer.AddDays(1).AddHours(18),
                    Descricao = "Implementação de lógica"
                }
            };
        }

        protected override Periodo ObterExemploEntidadeValidaParaAtualizacao()
        {
            return new Periodo()
            {
                Id = new Guid("6452a5e4-347d-4837-9ae4-4122db50721a"),
                HorarioDeInicio = DataQualquer.AddHours(8),
                HorarioDeEncerramento = DataQualquer.AddHours(12),
                Descricao = "Planejamento de testes",
                FolhaId = new Guid("b31ad33b-e715-48bb-8fc0-522eb98cb292")
            };
        }

        protected override Periodo ObterExemploEntidadeValidaParaInsercao()
        {
            return new Periodo()
            {
                HorarioDeInicio = DataQualquer.AddHours(14),
                HorarioDeEncerramento = DataQualquer.AddHours(15),
                Descricao = "Preparação de ambiente",
                FolhaId = new Guid("27384238-1f4e-42a4-a0c8-7e3b06b527ae")
            };
        }
    }
}