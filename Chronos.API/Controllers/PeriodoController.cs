using System;
using Chronos.API.Dados;
using Chronos.API.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Chronos.API.Controllers
{
    public class PeriodoController : Controller, IEntidadeApi<Periodo>
    {
        private const string NomeDaRotaDeConsulta = "ConsultarPeriodo";

        private readonly IRepositorio _repositorio;
        public PeriodoController(IRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var periodos = _repositorio.Periodos;
            return Ok(periodos);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            if (!_repositorio.Periodos.ConstaNoBanco(id))
                return NotFound();

            var periodo = _repositorio.Periodos
                .PorId(id);

            return Ok(periodo);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Periodo periodo)
        {
            if (periodo == null || !periodo.EstaValidoParaInsercao)
                return BadRequest();

            _repositorio.Acrescentar(periodo);

            return CreatedAtRoute(NomeDaRotaDeConsulta, periodo.Id, periodo);
        }

        [HttpPut]
        public IActionResult Put([FromBody] Periodo periodo)
        {
            if (periodo == null || !periodo.EstaValidoParaAtualizacao)
                return BadRequest();

            if (!_repositorio.Periodos.ConstaNoBanco(periodo.Id))
                return NotFound();

            _repositorio.Atualizar(periodo);

            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(Guid id)
        {
            if (!_repositorio.Periodos.ConstaNoBanco(id))
                return NotFound();

            _repositorio.Remover<Periodo>(id);

            return NoContent();
        }
    }
}