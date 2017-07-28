using System;
using System.Threading;
using System.Threading.Tasks;

namespace DevInstinct.Patterns.UnitOfWorkPattern
{
    public class UnitOfWorkScope<TUnitOfWork> : IUnitOfWorkScope<TUnitOfWork>
        where TUnitOfWork : class, IUnitOfWork
    {
        static AsyncLocal<TUnitOfWork> _ambiantUnitOfWork = new AsyncLocal<TUnitOfWork>();

        internal UnitOfWorkScope(IUnitOfWorkFactory<TUnitOfWork> uowFactory, bool readOnly = false)
        {
            Initialize(() => uowFactory.Create(), readOnly);
        }

        //internal UnitOfWorkScope(IUnitOfWorkFactory<TUnitOfWork> uowFactory, string connectionString, bool readOnly = false)
        //{
        //    Initialize(() => uowFactory.Create(connectionString), readOnly);
        //}

        //internal UnitOfWorkScope(IUnitOfWorkFactory<TUnitOfWork> uowFactory, IConnectionManager existingConnection, bool readOnly = false)
        //{
        //    Initialize(() => uowFactory.Create(existingConnection), readOnly);
        //}

        private void Initialize(Func<TUnitOfWork> factoryCall, bool readOnly)
        {
            if (_ambiantUnitOfWork.Value == null) // Assumes no parallelism
            {
                _ambiantUnitOfWork.Value = factoryCall();
                _ambiantUnitOfWork.Value.ReadOnly = readOnly;
                _ambiantUnitOfWork.Value.CanCommit = false; // Commit directly thru the Unit of Work is forbidden; must use the Scope Commit.
                OwnsUnitOfWork = true;
            }
            else if (_ambiantUnitOfWork.Value.ReadOnly != readOnly)
                throw new InvalidOperationException("Can't open a read-only scope in a write scope nor vice versa."); // TODO: Can't open a read-only scope in a write scope nor vice versa. Need to store a stack to track re-entrant scopes configurations.

            ReadOnly = readOnly;
            CanCommit = true; // We allow inner scopes to commit once, but that call is ignored if OwnsUnitOfWork is false.
            UnitOfWork = _ambiantUnitOfWork.Value;
        }

        public bool OwnsUnitOfWork { get; private set; }
        public bool ReadOnly { get; private set; }
        public bool CanCommit { get; private set; }

        public TUnitOfWork UnitOfWork { get; set; }

        public void Commit()
        {
            if (!CanCommit)
                throw new InvalidOperationException("Unit of Work Scope already committed.");
            CanCommit = false;

            if (OwnsUnitOfWork)
            {
                UnitOfWork.CanCommit = true;
                UnitOfWork.Commit();
                UnitOfWork.CanCommit = false;
            }
        }

        public Task CommitAsync()
        {
            if (!CanCommit)
                throw new InvalidOperationException("Unit of Work Scope already committed.");
            CanCommit = false;

            if (OwnsUnitOfWork)
            {
                UnitOfWork.CanCommit = true;
                Task task = UnitOfWork.CommitAsync();
                UnitOfWork.CanCommit = false;
                return task;
            }
            else
                return Task.CompletedTask;
        }

        public Task CommitAsync(CancellationToken cancellationToken)
        {
            if (!CanCommit)
                throw new InvalidOperationException("Unit of Work Scope already committed.");
            CanCommit = false;

            if (OwnsUnitOfWork)
            {
                UnitOfWork.CanCommit = true;
                Task task = UnitOfWork.CommitAsync(cancellationToken);
                UnitOfWork.CanCommit = false;
                return task;
            }
            else
                return Task.CompletedTask;
        }

        #region Pattern: IDisposable (base class)

        /// <summary>
        /// Each class has its own disposed flag (defensive implementation).
        /// </summary>
        /// <remarks>
        /// This flag must be checked in every public methods and ObjectDisposedException thrown if it is set to true.
        /// </remarks>
        private bool disposed;

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <remarks>
        /// Dispose(bool isDisposing) executes in two distinct scenarios.
        /// If disposing equals true, the method has been called directly
        /// or indirectly by a user's code. Managed and unmanaged resources
        /// can be disposed.
        /// If disposing equals false, the method has been called by the
        /// runtime from inside the finalizer and you should not reference
        /// other objects. Only unmanaged resources can be disposed.
        /// </remarks>
        /// <param name="isDisposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool isDisposing)
        {
            // Check to see if Dispose has already been called.
            if (disposed)
                return;

            if (isDisposing)
            {
                if (OwnsUnitOfWork)
                {
                    UnitOfWork.Dispose();
                    _ambiantUnitOfWork.Value = null;
                }
            }

            // TODO: free unmanaged resources here.

            // Indicate that disposing has been done.
            disposed = true;
        }


        /// <summary>
        /// Finalizer.
        /// </summary>
        /// <remarks>
        /// TODO: uncomment only if this class has unmanaged resources.
        /// </remarks>
        // ~UnitOfWorkScope()
        // {
        // 	Dispose(false);
        // }

        #endregion Pattern: IDisposable (base class)
    }
}
