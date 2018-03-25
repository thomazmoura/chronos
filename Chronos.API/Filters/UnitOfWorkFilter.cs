using System.Threading.Tasks;
using Chronos.API.Dados;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Chronos.API.Filters
{
    public class UnitOfWorkFilter : IAsyncActionFilter
    {
        private readonly IUnitOfWork _unitOfWork;
        public UnitOfWorkFilter(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultadoExecucao = await next();

            if (resultadoExecucao.Exception == null)
                await _unitOfWork.SalvarAlteracoes();
        }
    }
}