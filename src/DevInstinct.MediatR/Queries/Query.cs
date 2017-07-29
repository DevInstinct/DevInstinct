using DevInstinct.Patterns.CQRSPattern;
using MediatR;

namespace DevInstinct.MediatR.Queries
{
    public interface IMediatRQuery<TResponse> : IRequest<TResponse>, IQuery<TResponse>
    {
    }

    public class Query<TResponse> : IRequest<TResponse>, IQuery<TResponse>
    {
    }
}
