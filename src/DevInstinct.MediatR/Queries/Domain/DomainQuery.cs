using DevInstinct.Patterns.TierPattern;

namespace DevInstinct.MediatR.Queries.Domain
{
    public class DomainQuery<TResponse> : Query<TResponse>, IDomainTier
    {
    }
}
