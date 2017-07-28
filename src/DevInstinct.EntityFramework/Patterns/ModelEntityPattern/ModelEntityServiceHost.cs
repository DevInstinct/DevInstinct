using DevInstinct.Patterns;
using DevInstinct.Patterns.UnitOfWorkPattern;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DevInstinct.EntityFramework.Patterns.ModelEntityPattern
{
    public abstract class ModelEntityServiceHost<TModel, TModelKeyType, TEntity, TEntityKeyType, TUnitOfWork>
        where TModel : class, IKey<TModelKeyType>
        where TModelKeyType : IEquatable<TModelKeyType>
        where TEntity : class, IKey<TEntityKeyType>
        where TEntityKeyType : IEquatable<TEntityKeyType>
        where TUnitOfWork : class, IUnitOfWork
    {
        protected ModelEntityServiceHost(IModelEntityService<TUnitOfWork> modelEntityService) { ModelEntityService = modelEntityService; }

        public IModelEntityService<TUnitOfWork> ModelEntityService { get; private set; }

        public virtual Task Save(TModel model, CancellationToken cancellationToken, EntityState entityState, bool updateModelAfterCommit = false)
            => ModelEntityService.Save<TModel, TModelKeyType, TEntity, TEntityKeyType>(model, cancellationToken, entityState, updateModelAfterCommit);
    }
}
