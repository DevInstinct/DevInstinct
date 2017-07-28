using DevInstinct.Patterns.UnitOfWorkPattern;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using DevInstinct.EntityFramework.Patterns.UnitOfWorkPattern;

namespace DevInstinct.EntityFramework.Data.Entity
{
    public abstract class DbUnitOfWork : DbContext, IUnitOfWork//, IDbConnectionManager // TODO: add GetEdm to proper inteface (not IUnitOfWork because Edm is EF/OData-specific).
    {
        protected DbUnitOfWork() { OwnsConnection = true; }
        //protected DbUnitOfWork(string connectionString) : base(connectionString) { OwnsConnection = true; }
        //protected DbUnitOfWork(IConnectionManager existingConnection) : this((IDbConnectionManager)existingConnection) { }
        //protected DbUnitOfWork(IDbConnectionManager existingConnection) : this(existingConnection.Connection) { }
        ////protected DbUnitOfWork(IDbConnection existingConnection, bool contextOwnsConnection = false) : base((DbConnection)existingConnection, contextOwnsConnection)
        //{
        //    OwnsConnection = contextOwnsConnection;
        //}

        public bool ReadOnly
        {
            get => !ChangeTracker.AutoDetectChangesEnabled;
            set => ChangeTracker.AutoDetectChangesEnabled = !value;
        }

        public event EventHandler OnCommit;

        public bool OwnsConnection { get; private set; }

        public bool CanCommit { get; set; } = true;

        //public virtual IEnumerable<UnitOfWorkEntityInterceptor> Interceptors { get; set; }

        /// <summary>
        /// Saves all changes made in this context to the underlying database.
        /// </summary>
        /// <returns>The number of objects written to the underlying database.</returns>
        public void Commit()
        {
            if (!CanCommit)
                throw new InvalidOperationException("Direct Unit of Work commit has been suppressed.");

            //Intercept();
            SaveChanges();
            RaiseOnCommit();
        }

        /// <summary>
        /// Asynchronously saves all changes made in this context to the underlying database.
        /// </summary>
        /// <returns>A task that represents the asynchronous save operation.  The task result contains the number of objects written to the underlying database.</returns>
        public virtual async Task CommitAsync()
        {
            if (!CanCommit)
                throw new InvalidOperationException("Direct Unit of Work commit has been suppressed.");

            //await InterceptAsync();
            await SaveChangesAsync();
            RaiseOnCommit();
            return;
        }

        /// <summary>
        /// Asynchronously saves all changes made in this context to the underlying database.
        /// </summary>
        /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous save operation.  The task result contains the number of objects written to the underlying database.</returns>
        public virtual async Task CommitAsync(CancellationToken cancellationToken)
        {
            if (!CanCommit)
                throw new InvalidOperationException("Direct Unit of Work commit has been suppressed.");

            //await InterceptAsync();
            await SaveChangesAsync(cancellationToken);
            RaiseOnCommit();
            return;
        }

        public void RaiseOnCommit()
        {
            if (OnCommit != null)
            {
                OnCommit(this, EventArgs.Empty);
                foreach (EventHandler v in OnCommit.GetInvocationList())
                    OnCommit -= v;
            }
        }

        //public abstract IEdmModel GetEdm();

//        public IDbConnection Connection
//        {
//            get
//            {
//                return Database.Connection;
//            }
//        }

//        private void Intercept()
//        {
//            if (Interceptors != null)
//                ChangeTracker.Entries().ForEach
//                (
//                    (entry) =>
//                    {
//                        Interceptors.ForEach
//                        (
//                            (interceptor) =>
//                            {
//                                switch (entry.State)
//                                {
//#warning "TODO: more meaningful exception?"
//                                    case EntityState.Added:
//                                        if (!interceptor.BeforeAddEntity(this, entry.Entity))
//                                            throw new InvalidOperationException();
//                                        break;
//                                    case EntityState.Modified:
//                                        if (!interceptor.BeforeUpdateEntity(this, entry.Entity))
//                                            throw new InvalidOperationException();
//                                        break;
//                                    case EntityState.Deleted:
//                                        if (!interceptor.BeforeDeleteEntity(this, entry.Entity))
//                                            throw new InvalidOperationException();
//                                        break;
//                                }
//                            }
//                        );
//                    }
//                );
//        }

        //private Task InterceptAsync()
        //{
        //    return Task.Run(() => Intercept());
        //}
    }
}
