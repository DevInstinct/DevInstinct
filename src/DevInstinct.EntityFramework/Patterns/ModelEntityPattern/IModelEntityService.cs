using DevInstinct.Patterns;
using DevInstinct.Patterns.UnitOfWorkPattern;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DevInstinct.EntityFramework.Patterns.ModelEntityPattern
{
    public interface IModelEntityService<TUnitOfWork>
        where TUnitOfWork : class, IUnitOfWork
    {
        Task Save<TModel, TModelKeyType, TEntity, TEntityKeyType>(TModel model, CancellationToken cancellationToken, EntityState entityState, bool updateModelAfterCommit = false)
            where TModel : class, IKey<TModelKeyType>
            where TModelKeyType : IEquatable<TModelKeyType>
            where TEntity : class, IKey<TEntityKeyType>
            where TEntityKeyType : IEquatable<TEntityKeyType>;

        IQueryable<TModel> ProjectTo<TModel, TEntity>(IQueryable<TEntity> source, params Expression<Func<TModel, object>>[] membersToExpand);
    }
}
