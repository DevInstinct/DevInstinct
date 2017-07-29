using AutoMapper.QueryableExtensions;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DevInstinct.MediatR.AutoMapper
{
    public abstract class EnumerableQueryProjectionHandler<TQuery, TModel, TEntity> :
        ICancellableAsyncRequestHandler<TQuery, IEnumerable<TModel>>
        where TQuery : IRequest<IEnumerable<TModel>>
    {

        public Task<IEnumerable<TModel>> Handle(TQuery message, CancellationToken cancellationToken)
        {
            var query = (IQueryable<TEntity>)message;
            return Task.FromResult(query.ProjectTo<TModel>().AsEnumerable()); // TODO: use ToListAsync() and get rid of Task when a sample with a real DBContext is available
        }
    }
}
