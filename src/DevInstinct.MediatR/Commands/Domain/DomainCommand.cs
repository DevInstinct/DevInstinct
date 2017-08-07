using DevInstinct.Patterns.TierPattern;

namespace DevInstinct.MediatR.Commands.Domain
{
    public class DomainCommand : Command, IDomainTier
    {
    }

    public class DomainCommand<TModel> : Command<TModel>, IDomainTier
    {
    }
}
