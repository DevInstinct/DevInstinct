using DevInstinct.Patterns.TierPattern;

namespace DevInstinct.MediatR.Commands.Business
{
    public class BusinessCommand : Command, IBusinessTier
    {
    }

    public class BusinessCommand<TModel> : Command<TModel>, IBusinessTier
    {
    }
}
