namespace DevInstinct.Patterns.CQRSPattern
{
    public interface ICommand : IResponsibility
    {
    }


    // TODO: should support ICommand<TResult> for real-life scenarios where commands must return results.

    public interface ICommand<TModel> : ICommand
    {
        TModel Model { get; set; }
    }
}
