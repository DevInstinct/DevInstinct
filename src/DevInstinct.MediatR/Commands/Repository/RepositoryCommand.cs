using DevInstinct.Patterns.TierPattern;

namespace DevInstinct.MediatR.Commands.Repository
{
    public class RepositoryCommand : Command, IRepositoryTier
    {
    }

    public class RepositoryCommand<TModel> : Command<TModel>, IRepositoryTier
    {
    }
}
