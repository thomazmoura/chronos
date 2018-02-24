using Chronos.API.Dados;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Chronos.API.Filters
{
    public class UnitOfWorkFilter : IActionFilter
    {
        private readonly IUnitOfWork _unitOfWork;
        public UnitOfWorkFilter(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception == null)
                _unitOfWork.SalvarAlteracoes();
        }

        public void OnActionExecuting(ActionExecutingContext context) { }
    }
}