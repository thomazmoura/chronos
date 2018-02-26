using System;
using Chronos.API.Dados;
using Chronos.API.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Chronos.API.Controllers
{
    [Route("api/folhas")]
    public class FolhaController : Controller, IEntidadeApi<Folha>
    {
        private const string NomeDaRotaDeConsulta = "ConsultarFolha";

        private readonly IRepositorio _repositorio;
        public FolhaController(IRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public IActionResult Get()
        {
            var folhas = _repositorio.Folhas;
            return Ok(folhas);
        }

        [HttpGet("{id}", Name = NomeDaRotaDeConsulta)]
        public IActionResult Get(Guid id)
        {
            if (!_repositorio.Folhas.ConstaNoBanco(id))
                return NotFound();

            var folha = _repositorio.Folhas.PorId(id);

            return Ok(folha);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Folha folha)
        {
            if (folha == null)
                return BadRequest();

            if (!folha.EstaValidoParaInsercao)
                return BadRequest();

            _repositorio.Acrescentar(folha);

            return CreatedAtRoute(NomeDaRotaDeConsulta, folha.Id, folha);
        }

        public IActionResult Put([FromBody] Folha folha)
        {
            if (folha == null)
                return BadRequest();

            if (!folha.EstaValidoParaAtualizacao)
                return BadRequest();

            if (!_repositorio.Folhas.ConstaNoBanco(folha.Id))
                return NotFound();

            _repositorio.Atualizar(folha);

            return NoContent();
        }

        public IActionResult Delete(Guid id)
        {
            if (!_repositorio.Folhas.ConstaNoBanco(id))
                return NotFound();

            _repositorio.Remover<Folha>(id);

            return NoContent();
        }
    }
}