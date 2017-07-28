using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DevInstinct.Patterns.UnitOfWorkPattern
{
    // Inspired from the Query Object Pattern:
    // http://coderkarl.wordpress.com/2012/05/02/the-query-object-pattern-2/
    public abstract class UnitOfWorkQuery<TIUnitOfWork, TEntity> : IQueryable<TEntity>
        where TIUnitOfWork : class, IUnitOfWork
    {
        protected UnitOfWorkQuery(TIUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork ?? throw new ArgumentNullException();
        }

        protected TIUnitOfWork UnitOfWork { get; private set; }

        public abstract IQueryable<TEntity> Query { get; }

        public IEnumerator<TEntity> GetEnumerator() => Query.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => Query.GetEnumerator();

        public Type ElementType => Query.ElementType;

        public Expression Expression => Query.Expression;

        public IQueryProvider Provider => Query.Provider;
    }
}
