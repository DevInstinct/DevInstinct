using DevInstinct.Patterns.TierPattern;

namespace DevInstinct.MediatR.Queries.Repository
{
    public class RepositoryQuery<TResponse> : Query<TResponse>, IRepositoryTier
    {
    }
}
