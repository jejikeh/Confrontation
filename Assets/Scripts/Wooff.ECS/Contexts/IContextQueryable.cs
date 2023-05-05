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
            var contextWhereQuery = ContextWhereQuery(query);
            var emptySelf = CreateEmptySelf();
            var context = emptySelf as IContext<T>;
            foreach (var contextQueryable in contextWhereQuery)
                context?.ContextAdd(contextQueryable);
            
            return context as IContextQueryable<T>;
        }

        public IContextQueryable<T> CreateEmptySelf();
    }
}