using DevInstinct.Patterns.CQRSPattern;
using MediatR;

namespace DevInstinct.MediatR.Commands
{
    public class Command : IRequest, ICommand
    {
    }

    public class Command<TModel> : IRequest, ICommand<TModel>
    {
        public TModel Model { get; set; }
    }
}
