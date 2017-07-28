using DevInstinct.Patterns.CQRSPattern;

namespace DevInstinct.MediatR.Commands.Repository
{
    public class CreateRepositoryCommand<TModel> : RepositoryCommand<TModel>, ICreateCommand<TModel>
    {
        public bool UpdateModelAfterCommit { get; set; }
    }
}
