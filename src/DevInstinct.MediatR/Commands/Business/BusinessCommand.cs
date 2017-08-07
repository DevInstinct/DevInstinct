using DevInstinct.Patterns.TierPattern;

namespace DevInstinct.MediatR.Commands.Business
{
    public class BusinessCommand : Command, IDomainTier
    {
    }

    public class BusinessCommand<TModel> : Command<TModel>, IDomainTier
    {
    }
}
