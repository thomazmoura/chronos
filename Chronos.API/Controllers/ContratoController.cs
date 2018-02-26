using System;
using Chronos.API.Dados;
using Chronos.API.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Chronos.API.Controllers
{
    [Route("api/contratos")]
    public class ContratoController : Controller, IEntidadeApi<Contrato>
    {
        private const string NomeDaRotaDeConsulta = "ConsultarContrato";

        private readonly IRepositorio _repositorio;
        public ContratoController(IRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public IActionResult Get()
        {
            var contratos = _repositorio.Contratos;
            return Ok(contratos);
        }

        [HttpGet("{id}", Name = NomeDaRotaDeConsulta)]
        public IActionResult Get(Guid id)
        {
            if (!_repositorio.Contratos.ConstaNoBanco(id))
                return NotFound();

            var contrato = _repositorio.Contratos.PorId(id);

            return Ok(contrato);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Contrato contrato)
        {
            if (contrato == null)
                return BadRequest();

            if (!contrato.EstaValidoParaInsercao)
                return BadRequest();

            _repositorio.Acrescentar(contrato);

            return CreatedAtRoute(NomeDaRotaDeConsulta, contrato.Id, contrato);
        }

        public IActionResult Put([FromBody] Contrato contrato)
        {
            if (contrato == null)
                return BadRequest();

            if (!contrato.EstaValidoParaAtualizacao)
                return BadRequest();

            if (!_repositorio.Contratos.ConstaNoBanco(contrato.Id))
                return NotFound();

            _repositorio.Atualizar(contrato);

            return NoContent();
        }

        public IActionResult Delete(Guid id)
        {
            if (!_repositorio.Contratos.ConstaNoBanco(id))
                return NotFound();

            _repositorio.Remover<Contrato>(id);

            return NoContent();
        }
    }
}