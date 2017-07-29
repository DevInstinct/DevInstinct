using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DevInstinct.MediatR.AutoMapper
{
    public abstract class SingleQueryProjectionHandler<TQuery, TModel, TEntity> :
        ICancellableAsyncRequestHandler<TQuery, TModel>
        where TQuery : IRequest<TModel>
    {
        public IMapper Mapper { get; }

        protected SingleQueryProjectionHandler(IMapper mapper)
        {
            Mapper = mapper;
        }

        public virtual Task<TModel> Handle(TQuery message, CancellationToken cancellationToken)
        {
            var query = (IQueryable<TEntity>)message;
            return Task.FromResult(query.ProjectTo<TModel>().SingleOrDefault()); // TODO: use ToListAsync() and get rid of Task when a sample with a real DBContext is available
        }
    }
}
