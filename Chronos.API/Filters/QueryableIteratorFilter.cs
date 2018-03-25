using System.Linq;
using System.Threading.Tasks;
using Chronos.API.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Chronos.API.Filters
{
    public class QueryableIteratorFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultadoExecucao = await next();
            var resultado = resultadoExecucao.Result as ObjectResult;

            if (resultado == null)
                return;

            var entidades = resultado.Value as IQueryable<Entidade>;

            if (entidades == null)
                return;

            (resultado as ObjectResult).Value = await entidades.EmMemoriaAsync();
        }
    }
}