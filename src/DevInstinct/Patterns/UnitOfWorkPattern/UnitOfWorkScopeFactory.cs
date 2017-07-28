namespace DevInstinct.Patterns.UnitOfWorkPattern
{
    public class UnitOfWorkScopeFactory
    {
        public static IUnitOfWorkScope<TUnitOfWork> Create<TUnitOfWork>(IUnitOfWorkFactory<TUnitOfWork> uowFactory, bool readOnly = false)
            where TUnitOfWork : class, IUnitOfWork
            => new UnitOfWorkScope<TUnitOfWork>(uowFactory, readOnly);

        //public static IUnitOfWorkScope<TUnitOfWork> Create<TUnitOfWork>(IUnitOfWorkFactory<TUnitOfWork> uowFactory, string connectionstring, bool readOnly = false)
        //    where TUnitOfWork : class, IUnitOfWork
        //    => new UnitOfWorkScope<TUnitOfWork>(uowFactory, connectionstring, readOnly);

        //public static IUnitOfWorkScope<TUnitOfWork> Create<TUnitOfWork>(IUnitOfWorkFactory<TUnitOfWork> uowFactory, IConnectionManager existingConnection, bool readOnly = false)
        //    where TUnitOfWork : class, IUnitOfWork
        //    => new UnitOfWorkScope<TUnitOfWork>(uowFactory, existingConnection, readOnly);
    }
}
