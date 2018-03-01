using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Chronos.API.Controllers;
using Chronos.API.Entidades;
using FluentAssertions;
using Xunit;

namespace Chronos.API.Testes.Controllers
{
    public class ApiTests
    {
        [Fact]
        public void NãoHáEntidadeApisSemSuiteDeTestesAssociada()
        {
            var entidades = ObterTiposDeEntidades();
            foreach (var entidade in entidades)
            {
                if (EntidadePossuiApi(entidade))
                {
                    var entidadeApiPossuiTeste = EntidadePossuiTesteDeApi(entidade);
                    entidadeApiPossuiTeste.Should().BeTrue($"{entidade} possui API, portanto deveria ter testes referentes");
                }

            }
        }

        private IEnumerable<Type> ObterTiposDeEntidades()
        {
            return Assembly.GetAssembly(this.GetType())
                .GetTypes()
                .Where(type => type.IsSubclassOf(typeof(Entidade))
                            && type != typeof(Entidade) && type != typeof(Entidade) && type != typeof(API.Entidades.Entidade<>));
        }

        private bool EntidadePossuiApi(Type entidadeType)
        {
            var tipoDaApi = typeof(IEntidadeApi<>).MakeGenericType(entidadeType);
            return Assembly.GetAssembly(this.GetType())
                .GetTypes()
                .Any(type => type.GetInterfaces().Contains(tipoDaApi));
        }

        private bool EntidadePossuiTesteDeApi(Type entidadeType)
        {
            var tipoDoTest = typeof(ApiControllerTests<>).MakeGenericType(entidadeType);
            return Assembly.GetAssembly(typeof(ApiTests))
                .GetTypes()
                .Any(type => type.IsSubclassOf(tipoDoTest));
        }
    }
}