using DevInstinct.EntityFramework.Data.Entity;
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
    // To use if Entity is used as Model
    public class EntityAsModelService<TUnitOfWork> : IModelEntityService<TUnitOfWork>
        where TUnitOfWork : class, IUnitOfWork
    {
        public EntityAsModelService(IUnitOfWorkFactory<TUnitOfWork> uowFactory)
        {
            UowFactory = uowFactory;
        }

        public IUnitOfWorkFactory<TUnitOfWork> UowFactory { get; private set; }

        public async Task Save<TModel, TModelKeyType, TEntity, TEntityKeyType>(TModel model, CancellationToken cancellationToken, EntityState entityState, bool updateModelAfterCommit = false)
            where TModel : class, IKey<TModelKeyType>
            where TModelKeyType : IEquatable<TModelKeyType>
            where TEntity : class, IKey<TEntityKeyType>
            where TEntityKeyType : IEquatable<TEntityKeyType>
        {
            using (IUnitOfWorkScope<TUnitOfWork> scope = UnitOfWorkScopeFactory.Create(UowFactory))
            {
                DbUnitOfWork dbContext = scope.UnitOfWork as DbUnitOfWork;
                if (dbContext == null)
                    throw new InvalidOperationException("Unsupported Unit of Work.");

                dbContext.Entry(model).State = entityState;
                await scope.CommitAsync(cancellationToken);
            }
        }

        public IQueryable<TModel> ProjectTo<TModel, TEntity>(IQueryable<TEntity> source, params Expression<Func<TModel, object>>[] membersToExpand)
        {
            return (IQueryable<TModel>)source;
        }
    }
}
