using System;
using System.Collections.Generic;
using Chronos.API.Controllers;
using Chronos.API.Entidades;

namespace Chronos.API.Testes.Controllers
{
    public class ContratoControllerTests : ApiControllerTests<Contrato>
    {
        private ContratoController _contratoController;
        protected override IEntidadeApi<Contrato> ObterApiController()
        {
            _contratoController = new ContratoController(_ambienteDeTeste.Repositorio);
            return _contratoController;
        }

        protected override Contrato ObterExemploEntidadeInvalidaParaAtualizacao()
        {
            return new Contrato();
        }

        protected override Contrato ObterExemploEntidadeInvalidaParaInsercao()
        {
            return new Contrato()
            {
                Id = Guid.NewGuid()
            };
        }

        protected override IEnumerable<Contrato> ObterExemploEntidades()
        {
            return new[]
            {
                new Contrato()
                {
                    ValorDaHora = 50
                },
                new Contrato()
                {
                    ValorDaHora = 75
                }
            };
        }

        protected override Contrato ObterExemploEntidadeValidaParaAtualizacao()
        {
            return new Contrato()
            {
                Id = new Guid("bcf0f387-a6ee-41d7-a142-1bf1e1cb4e17"),
                ValorDaHora = 60
            };
        }

        protected override Contrato ObterExemploEntidadeValidaParaInsercao()
        {
            return new Contrato()
            {
                ValorDaHora = 75
            };
        }
    }
}