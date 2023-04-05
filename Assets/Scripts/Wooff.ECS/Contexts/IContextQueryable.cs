using System;
using System.Collections.Generic;
using System.Linq;

namespace Wooff.ECS.Contexts
{
    public interface IContextQueryable<out T> : IEnumerable<T>
    {
        public IQueryable<T1> ContextSelectQuery<T1>(Func<T, T1> query);
        public IQueryable<T> ContextWhereQuery(Func<T, bool> query);

        public IQueryable<IContextQueryable<T1>?> ContextSelectQueryable<T1>(Func<T, T1> query)
        {
            return ContextSelectQuery(query).Select(x => x as IContextQueryable<T1>);
        }
        
        public IContextQueryable<T>? ContextWhereQueryable(Func<T, bool> query)
        {
            var w = ContextWhereQuery(query);
            var t = CreateEmptySelf();
            var c = t as IContext<T>;
            foreach (var contextQueryable in w)
                c?.ContextAdd(contextQueryable);
            
            return c as IContextQueryable<T>;
        }

        public IContextQueryable<T> CreateEmptySelf();
    }
}