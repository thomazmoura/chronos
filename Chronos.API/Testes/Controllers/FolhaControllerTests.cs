using System;
using System.Collections.Generic;
using Chronos.API.Controllers;
using Chronos.API.Entidades;

namespace Chronos.API.Testes.Controllers
{
    public class FolhaControllerTests : ApiControllerTests<Folha>
    {
        private readonly DateTime DataQualquer = new DateTime(2018, 04, 01);

        private FolhaController _folhaController;
        protected override IEntidadeApi<Folha> ObterApiController()
        {
            _folhaController = new FolhaController(_ambienteDeTeste.Repositorio);
            return _folhaController;
        }

        protected override Folha ObterExemploEntidadeInvalidaParaAtualizacao()
        {
            return new Folha();
        }

        protected override Folha ObterExemploEntidadeInvalidaParaInsercao()
        {
            return new Folha()
            {
                Id = new Guid("384a65f4-51f1-45dd-ba6d-ba24e9ed2858")
            };
        }

        protected override IEnumerable<Folha> ObterExemploEntidades()
        {
            return new[]
            {
                new Folha()
                {
                    Id = new Guid("95f82e1f-2e75-4fa2-9e6e-4ba759f663a6"),
                    ContratoId = new Guid("c8646ba6-0a1b-44ec-937b-8c7efe29c68d"),
                    DataInicial = DataQualquer,
                    DataFinal = DataQualquer.AddDays(20),
                    QuantidadePrevistaDeDiasUteis = 12
                },
                new Folha()
                {
                    Id = new Guid("96191739-a647-4e95-9890-5135694fd8c6"),
                    ContratoId = new Guid("c8646ba6-0a1b-44ec-937b-8c7efe29c68d"),
                    DataInicial = DataQualquer.AddDays(30),
                    DataFinal = DataQualquer.AddDays(55),
                    QuantidadePrevistaDeDiasUteis = 20
                },
            };
        }

        protected override Folha ObterExemploEntidadeValidaParaAtualizacao()
        {
            return new Folha()
            {
                Id = new Guid("95f82e1f-2e75-4fa2-9e6e-4ba759f663a6"),
                ContratoId = new Guid("2fcd71a5-57dc-445f-94e4-107cf76b2002"),
                DataInicial = DataQualquer.AddDays(-20),
                DataFinal = DataQualquer,
                QuantidadePrevistaDeDiasUteis = 12
            };
        }

        protected override Folha ObterExemploEntidadeValidaParaInsercao()
        {
            return new Folha()
            {
                ContratoId = new Guid("2fcd71a5-57dc-445f-94e4-107cf76b2002"),
                DataInicial = DataQualquer.AddDays(-30),
                DataFinal = DataQualquer.AddDays(-10),
                QuantidadePrevistaDeDiasUteis = 13
            };
        }
    }
}