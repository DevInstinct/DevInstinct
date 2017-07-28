namespace DevInstinct.Patterns.CQRSPattern
{
    public interface ICommand : IResponsibility
    {
    }

    public interface ICommand<TModel> : ICommand
    {
        TModel Model { get; set; }
    }
}
