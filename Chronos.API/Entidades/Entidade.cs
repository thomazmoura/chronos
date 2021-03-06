using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Chronos.API.Entidades
{
    public abstract class Entidade
    {
        public Guid Id { get; set; }
        internal virtual bool EstaValidoParaInsercao => Id == default(Guid) && PossuiTodosOsCamposObrigatorios;
        internal virtual bool EstaValidoParaAtualizacao => Id != default(Guid) && PossuiTodosOsCamposObrigatorios;
        internal abstract bool PossuiTodosOsCamposObrigatorios { get; }
    }

    public abstract class Entidade<TEntidade> : Entidade where TEntidade : Entidade
    {
        public virtual bool EquivaleA(TEntidade outraEntidade)
        {
            return Id == outraEntidade.Id;
        }
    }

    public static class EntidadeExtensions
    {
        public static List<TEntidade> EmMemoria<TEntidade>(this IQueryable<TEntidade> entidades) where TEntidade : Entidade
        {
            return entidades.ToList();
        }

        public static Task<List<TEntidade>> EmMemoriaAsync<TEntidade>(this IQueryable<TEntidade> entidades) where TEntidade : Entidade
        {
            return entidades.ToListAsync();
        }


        public static bool PossuiAlgumValor(this IQueryable<Entidade> entidades)
        {
            return entidades.Any();
        }

        public static bool ConstaNoBanco(this IQueryable<Entidade> entidades, Guid id)
        {
            return entidades.Any(entidade => entidade.Id == id);
        }

        public static TEntidade PorId<TEntidade>(this IQueryable<TEntidade> entidades, Guid id) where TEntidade : Entidade
        {
            return entidades.SingleOrDefault(entidade => entidade.Id == id);
        }
    }
}