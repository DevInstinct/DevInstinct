using DevInstinct.Patterns.CQRSPattern;

namespace DevInstinct.MediatR.Commands.Repository
{
    public class UpdateRepositoryCommand<TModel> : RepositoryCommand<TModel>, IUpdateCommand<TModel>
    {
        public bool UpdateModelAfterCommit { get; set; }
    }
}
