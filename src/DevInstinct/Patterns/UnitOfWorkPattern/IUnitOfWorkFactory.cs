namespace DevInstinct.Patterns.UnitOfWorkPattern
{
    public interface IUnitOfWorkFactory<TUnitOfWork>
        where TUnitOfWork : IUnitOfWork
    {
        TUnitOfWork Create();
        //TUnitOfWork Create(string connectionString);
        //TUnitOfWork Create(IConnectionManager existingConnection);
    }
}
