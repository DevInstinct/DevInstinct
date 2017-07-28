namespace DevInstinct.Patterns.CQRSPattern
{
    public interface IQuery : IResponsibility
    {
    }

    public interface IQuery<out TResult> : IQuery
    {
    }
}
