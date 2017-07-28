using AutoMapper;
using AutoMapper.QueryableExtensions;
using DevInstinct.EntityFramework.Data.Entity;
using DevInstinct.EntityFramework.Patterns.ModelEntityPattern;
using DevInstinct.Patterns;
using DevInstinct.Patterns.UnitOfWorkPattern;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DevInstinct.EntityFramework.AutoMapper.Patterns.ModelEntityPattern
{
    // To use if Model != Entity and using AutoMapper
    public class AutoMapperModelEntityService<TUnitOfWork> : IModelEntityService<TUnitOfWork>
        where TUnitOfWork : class, IUnitOfWork
    {
        public AutoMapperModelEntityService(IUnitOfWorkFactory<TUnitOfWork> uowFactory, IMapper mapper, IConfigurationProvider mapperConfiguration)
        {
            UowFactory = uowFactory;
            Mapper = mapper;
            MapperConfiguration = mapperConfiguration;
        }

        public IUnitOfWorkFactory<TUnitOfWork> UowFactory { get; private set; }

        public IMapper Mapper { get; private set; }

        public IConfigurationProvider MapperConfiguration { get; }

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

                var entityEntry =
                    dbContext.ChangeTracker.Entries<TEntity>()
                    .Where(e => !e.Entity.Id.Equals(default(TEntityKeyType)))
                    .Where(e => typeof(TEntityKeyType) == typeof(TModelKeyType) ? e.Entity.Id.Equals(model.Id) : e.Entity.Id.ToString() == model.Id.ToString())
                    .SingleOrDefault();
                if (entityEntry == null)
                {
                    TEntity entity = Mapper.Map<TModel, TEntity>(model);
                    entityEntry = dbContext.Entry(entity);
                }

                entityEntry.State = entityState;
                if (updateModelAfterCommit)
                    switch (entityState)
                    {
                        case EntityState.Added:
                        case EntityState.Modified:
                            dbContext.OnCommit += (db, e) => Mapper.Map(entityEntry.Entity, model); // Refresh model in case of DB-assigned values (Identity/timestamp/etc.). 
                            break;
                    }

                await scope.CommitAsync(cancellationToken);
            }
        }

        public IQueryable<TModel> ProjectTo<TModel, TEntity>(IQueryable<TEntity> source, params Expression<Func<TModel, object>>[] membersToExpand)
        {
            using (IUnitOfWorkScope<TUnitOfWork> scope = UnitOfWorkScopeFactory.Create(UowFactory))
            {
                return source.ProjectTo(MapperConfiguration, membersToExpand);
            }
        }
    }
}
