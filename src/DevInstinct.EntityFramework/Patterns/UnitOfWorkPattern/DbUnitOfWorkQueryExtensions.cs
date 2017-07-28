using DevInstinct.Patterns.UnitOfWorkPattern;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace DevInstinct.EntityFramework.Patterns.UnitOfWorkPattern
{
    public static class DbUnitOfWorkQueryExtensions
    {
        /// <remarks>
        /// The Include member method of DbQuery is different than the Include extension method for IQueryable.
        /// By providing an Include method here, we make sure that the proper Include method is called if Query is of type DbQuery.
        /// (Member methods have priority over extension methods.)
        /// </remarks>
        public static IQueryable<TEntity> Include<TUnitOfWork, TEntity>(this UnitOfWorkQuery<TUnitOfWork, TEntity> uowQuery, string path)
            where TUnitOfWork : class, IUnitOfWork
            where TEntity : class
        {
            return uowQuery.Query.Include(path);
        }

        public static IQueryable<TEntity> IncludePath<TUnitOfWork, TEntity>(this UnitOfWorkQuery<TUnitOfWork, TEntity> uowQuery, params string[] propertyNames)
            where TUnitOfWork : class, IUnitOfWork
            where TEntity : class
        {
            return uowQuery.Query.Include(string.Join(".", propertyNames));
        }

        public static IQueryable<TEntity> Include<TUnitOfWork, TEntity, TProperty>(this UnitOfWorkQuery<TUnitOfWork, TEntity> uowQuery, Expression<Func<TEntity, TProperty>> path)
            where TUnitOfWork : class, IUnitOfWork
            where TEntity : class
        {
            return uowQuery.Query.Include(path);
        }
    }
}
