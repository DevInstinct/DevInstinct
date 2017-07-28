using DevInstinct.EntityFramework.Patterns.ModelEntityPattern;
using DevInstinct.MediatR.Commands.Repository;
using DevInstinct.Patterns;
using DevInstinct.Patterns.UnitOfWorkPattern;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DevInstinct.MediatR.EntityFramework.CommandHandlers
{
    public abstract class RepositoryCommandHandler<TModel, TModelKeyType, TEntity, TEntityKeyType, TUnitOfWork> :
        ModelEntityServiceHost<TModel, TModelKeyType, TEntity, TEntityKeyType, TUnitOfWork>,
        ICancellableAsyncRequestHandler<CreateRepositoryCommand<TModel>>,
        ICancellableAsyncRequestHandler<UpdateRepositoryCommand<TModel>>,
        ICancellableAsyncRequestHandler<DeleteRepositoryCommand<TModel>>,
        ICancellableAsyncRequestHandler<UnmodifyRepositoryCommand<TModel>>
        where TModel : class, IKey<TModelKeyType>
        where TModelKeyType : IEquatable<TModelKeyType>
        where TEntity : class, IKey<TEntityKeyType>
        where TEntityKeyType : IEquatable<TEntityKeyType>
        where TUnitOfWork : class, IUnitOfWork
    {
        protected RepositoryCommandHandler(IModelEntityService<TUnitOfWork> modelEntityService) : base(modelEntityService) { }

        public virtual Task Handle(CreateRepositoryCommand<TModel> command, CancellationToken cancellationToken) => Save(command.Model, cancellationToken, EntityState.Added, command.UpdateModelAfterCommit);

        public virtual Task Handle(UpdateRepositoryCommand<TModel> command, CancellationToken cancellationToken) => Save(command.Model, cancellationToken, EntityState.Modified, command.UpdateModelAfterCommit);

        public virtual Task Handle(DeleteRepositoryCommand<TModel> command, CancellationToken cancellationToken) => Save(command.Model, cancellationToken, EntityState.Deleted);

        public virtual Task Handle(UnmodifyRepositoryCommand<TModel> command, CancellationToken cancellationToken) => Save(command.Model, cancellationToken, EntityState.Unchanged);
    }
}
