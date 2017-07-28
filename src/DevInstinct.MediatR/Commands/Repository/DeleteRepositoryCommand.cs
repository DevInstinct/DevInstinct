using DevInstinct.Patterns.CQRSPattern;

namespace DevInstinct.MediatR.Commands.Repository
{
    public class DeleteRepositoryCommand<TModel> : RepositoryCommand<TModel>, IDeleteCommand<TModel>
    {
    }
}
