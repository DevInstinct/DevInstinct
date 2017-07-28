using DevInstinct.Patterns.CQRSPattern;

namespace DevInstinct.MediatR.Commands.Repository
{
    public class UnmodifyRepositoryCommand<TModel> : RepositoryCommand<TModel>, IUnmodifyCommand<TModel>
    {
    }
}
