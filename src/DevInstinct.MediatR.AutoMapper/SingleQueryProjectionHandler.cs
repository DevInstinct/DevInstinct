using AutoMapper;
using AutoMapper.QueryableExtensions;
using DevInstinct.MediatR.Queries;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DevInstinct.MediatR.AutoMapper
{
    public abstract class SingleQueryProjectionHandler<TQuery, TModel, TEntity, TQueryObject> :
        ICancellableAsyncRequestHandler<TQuery, TModel>
        where TQuery : Query<TModel>, IRequest<TModel>
        where TQueryObject : IQueryable<TEntity>
    {
        public IMapper Mapper { get; }
        public TQueryObject QueryObject { get; }

        protected SingleQueryProjectionHandler(IMapper mapper, TQueryObject queryObject)
        {
            Mapper = mapper;
            QueryObject = queryObject;
        }

        public virtual Task<TModel> Handle(TQuery message, CancellationToken cancellationToken)
        {
            Mapper.Map(message, QueryObject);
            return Task.FromResult(QueryObject.ProjectTo<TModel>().SingleOrDefault()); // TODO: use ToListAsync() and get rid of Task when a sample with a real DBContext is available
        }
    }
}
