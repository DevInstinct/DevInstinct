using DevInstinct.Patterns.TierPattern;

namespace DevInstinct.MediatR.Queries.Business
{
    public class BusinessQuery<TResponse> : Query<TResponse>, IDomainTier
    {
    }
}
