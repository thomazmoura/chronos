using System;
using Chronos.API.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Chronos.API.Controllers
{
    public interface IEntidadeApi<TEntidade> where TEntidade : Entidade<TEntidade>
    {
        IActionResult Get();
        IActionResult Get(Guid id);
        IActionResult Post([FromBody] TEntidade entidade);
        IActionResult Put([FromBody] TEntidade entidade);
        IActionResult Delete(Guid id);
    }
}