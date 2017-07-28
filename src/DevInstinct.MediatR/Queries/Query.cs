using DevInstinct.Patterns.CQRSPattern;
using MediatR;

namespace DevInstinct.MediatR.Queries
{
    public class Query<TResponse> : IRequest<TResponse>, IQuery<TResponse>
    {
    }
}
