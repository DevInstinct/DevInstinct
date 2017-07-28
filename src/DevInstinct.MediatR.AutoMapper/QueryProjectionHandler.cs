using AutoMapper.QueryableExtensions;
using DevInstinct.MediatR.Queries;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DevInstinct.MediatR.AutoMapper
{
    public abstract class QueryProjectionHandler<TQuery, TModel, TEntity, TQueryObject> :
        ICancellableAsyncRequestHandler<TQuery, IEnumerable<TModel>>
        where TQuery : Query<IEnumerable<TModel>>, IRequest<IEnumerable<TModel>>
        where TQueryObject : IQueryable<TEntity>
    {
        public TQueryObject QueryObject { get; }

        protected QueryProjectionHandler(TQueryObject query)
        {
            QueryObject = query;
        }

        public Task<IEnumerable<TModel>> Handle(TQuery message, CancellationToken cancellationToken)
        {
            return Task.FromResult(QueryObject.ProjectTo<TModel>().AsEnumerable()); // TODO: use ToListAsync() and get rid of Task when a sample with a real DBContext is available
        }
    }
}
