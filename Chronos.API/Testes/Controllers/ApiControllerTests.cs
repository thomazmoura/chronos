using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Chronos.API.Controllers;
using Chronos.API.Entidades;
using Chronos.API.Testes.Configuracao;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Chronos.API.Testes.Controllers
{
    public abstract class ApiControllerTests<TEntidade> where TEntidade : Entidade<TEntidade>, new()
    {
        protected readonly string _chaveDoBanco;
        protected abstract IEntidadeApi<TEntidade> ObterApiController();
        protected abstract IEnumerable<TEntidade> ObterExemploEntidades();
        protected abstract TEntidade ObterExemploEntidadeInvalidaParaInsercao();
        protected abstract TEntidade ObterExemploEntidadeValidaParaInsercao();
        protected abstract TEntidade ObterExemploEntidadeInvalidaParaAtualizacao();
        protected abstract TEntidade ObterExemploEntidadeValidaParaAtualizacao();

        protected readonly AmbienteDeTeste _ambienteDeTeste;
        protected IEntidadeApi<TEntidade> _controller;
        protected ApiControllerTests()
        {
            _chaveDoBanco = Guid.NewGuid().ToString();
            _ambienteDeTeste = AmbienteDeTeste.NovoAmbiente(_chaveDoBanco);
            _controller = ObterApiController();
        }

        protected virtual bool CompararEntidades(IEnumerable<TEntidade> entidadesObtidas, IEnumerable<TEntidade> entidadesEsperadas)
        {
            if (entidadesObtidas.Count() != entidadesEsperadas.Count())
                return false;

            foreach (var entidadeEsperada in entidadesObtidas)
                if (!entidadesEsperadas.Any(entidadeObtida => entidadeObtida.EquivaleA(entidadeEsperada)))
                    return false;

            return true;
        }
        protected virtual void PersistirEntidades(IEnumerable<Entidade> entidades)
        {
            var ambienteDeTeste = AmbienteDeTeste.NovoAmbiente(_chaveDoBanco);
            ambienteDeTeste.Contexto.AddRange(entidades);
            ambienteDeTeste.Contexto.SaveChanges();
        }
        protected virtual void PersistirEntidade(Entidade entidade)
        {
            PersistirEntidades(new[] { entidade });
        }
        protected virtual IEnumerable<TEntidade> ConsultarEntidadesPersistidas()
        {
            var ambienteDeTeste = AmbienteDeTeste.NovoAmbiente(_chaveDoBanco);
            return ambienteDeTeste.Contexto.Set<TEntidade>().ToList();
        }
        protected virtual TEntidade ConsultarEntidadePersistida()
        {
            return ConsultarEntidadesPersistidas().Single();
        }
        protected virtual TEntidade ConsultarEntidadeLocal(Guid id)
        {
            return _ambienteDeTeste.Contexto.Find<TEntidade>(id);
        }
        protected virtual EntityState ConsultarEstadoDeEntidadeLocal(Guid id)
        {
            var entidade = _ambienteDeTeste.Contexto.Find<TEntidade>(id);
            var entry = _ambienteDeTeste.Contexto.Entry(entidade);
            return entry.State;
        }

        [Fact]
        public void Get_RetornaListaVazia_QuandoNãoHáEntidadesNoBanco()
        {
            var resultado = _controller.Get() as OkObjectResult;
            var entidades = resultado.Value as IEnumerable<TEntidade>;

            entidades.Should().BeEmpty();
        }

        [Fact]
        public void Get_RetornaEntidadesRegistradas_QuandoHáEntidadesNoBanco()
        {
            var entidadesPersistidas = ObterExemploEntidades();
            PersistirEntidades(entidadesPersistidas);

            _controller = ObterApiController();
            var resultado = _controller.Get() as OkObjectResult;
            var entidades = resultado.Value as IEnumerable<TEntidade>;

            CompararEntidades(entidades, entidadesPersistidas);
        }

        [Fact]
        public void GetComId_RetornaNotFound_QuandoAEntidadeNãoExiste()
        {
            var idAusenteNoBanco = new Guid("930636ba-704d-4a01-95e2-96bb7283daee");

            var resultado = _controller.Get(idAusenteNoBanco);

            resultado.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void GetComId_RetornaEntidade_QuandoElaExiste()
        {
            var entidades = ObterExemploEntidades();
            PersistirEntidades(entidades);
            var entidadeDeExemplo = entidades.First();

            var resultado = _controller.Get(entidadeDeExemplo.Id) as OkObjectResult;
            var entidade = resultado.Value as TEntidade;

            entidade.EquivaleA(entidadeDeExemplo).Should().BeTrue();
        }

        [Fact]
        public void Post_RetornaBadRequest_QuandoAEntidadeNãoEstáVálidaParaInserção()
        {
            var entidadeDeExemplo = ObterExemploEntidadeInvalidaParaInsercao();

            var resultado = _controller.Post(entidadeDeExemplo);

            resultado.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public void Post_RetornaCreatedAtRoute_QuandoAEntidadeEstáVálidaParaInserção()
        {
            var entidadeDeExemplo = ObterExemploEntidadeValidaParaInsercao();

            var resultado = _controller.Post(entidadeDeExemplo) as CreatedAtRouteResult;
            var entidade = resultado.Value as TEntidade;
            var id = resultado.RouteValues["Id"].As<Guid>();

            entidade.Should().NotBeNull();
            entidade.EquivaleA(entidadeDeExemplo).Should().BeTrue();
        }

        [Fact]
        public void Post_MarcaEntidadeParaInserção_QuandoElaEstáVálidaParaInserção()
        {
            var entidadeASerInserida = ObterExemploEntidadeValidaParaInsercao();

            var resultado = _controller.Post(entidadeASerInserida) as CreatedAtRouteResult;
            var estadoDaEntidadeAposInsercao = ConsultarEstadoDeEntidadeLocal(entidadeASerInserida.Id);

            estadoDaEntidadeAposInsercao.Should().Be(EntityState.Added);
        }

        [Fact]
        public void Put_RetornaBadRequest_QuandoAEntidadeNãoEstáVálidaParaAtualização()
        {
            var entidadeDeExemplo = ObterExemploEntidadeInvalidaParaAtualizacao();

            var resultado = _controller.Put(entidadeDeExemplo);

            resultado.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public void Put_RetornaNotFound_QuandoAIdInformadaNãoConstaNoBanco()
        {
            var entidadeDeExemplo = ObterExemploEntidadeValidaParaAtualizacao();
            entidadeDeExemplo.Id = new Guid("9e27da53-307e-4d02-9af6-4cfb9bd47607");

            var resultado = _controller.Put(entidadeDeExemplo);

            resultado.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void Put_RetornaNoContentResult_QuandoAEntidadeEstáVálidaParaAtualização()
        {
            var entidadeDeExemplo = ObterExemploEntidadeValidaParaAtualizacao();
            PersistirEntidade(entidadeDeExemplo);

            var resultado = _controller.Put(entidadeDeExemplo);

            resultado.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public void Put_MarcaAEntidadeParaAtualização_QuandoElaEstáVálidaParaAtualização()
        {
            var entidadesDeExemplo = ObterExemploEntidades().Take(2);
            var entidadeOriginal = entidadesDeExemplo.First();
            var entidadeRevisada = entidadesDeExemplo.Last();
            PersistirEntidade(entidadeOriginal);
            entidadeRevisada.Id = entidadeOriginal.Id;

            var resultado = _controller.Put(entidadeRevisada);
            var estadoDaEntidadeAposAtualizacao = ConsultarEstadoDeEntidadeLocal(entidadeRevisada.Id);

            estadoDaEntidadeAposAtualizacao.Should().Be(EntityState.Modified);
        }

        [Fact]
        public void Delete_RetornaNotFound_QuandoAEntidadeNãoExiste()
        {
            var idAusenteNoBanco = new Guid("7e45797e-158b-4f8a-aa57-d1c588f40c10");

            var resultado = _controller.Delete(idAusenteNoBanco);

            resultado.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void Delete_RetornaNoContent_QuandoAEntidadeExiste()
        {
            var entidadeDeExemplo = ObterExemploEntidadeValidaParaInsercao();
            PersistirEntidade(entidadeDeExemplo);
            var idEntidade = entidadeDeExemplo.Id;

            var resultado = _controller.Delete(idEntidade);

            resultado.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public void Delete_MarcaAEntidadeParaRemoção_QuandoElaExiste()
        {
            var entidadeDeExemplo = ObterExemploEntidadeValidaParaInsercao();
            PersistirEntidade(entidadeDeExemplo);
            var idEntidade = entidadeDeExemplo.Id;

            var resultado = _controller.Delete(idEntidade);
            var estadoDaEntidadeASerRemovida = ConsultarEstadoDeEntidadeLocal(idEntidade);

            estadoDaEntidadeASerRemovida.Should().Be(EntityState.Deleted);
        }
    }
}