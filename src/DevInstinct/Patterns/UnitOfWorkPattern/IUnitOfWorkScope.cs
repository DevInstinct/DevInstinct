using System;
using System.Threading;
using System.Threading.Tasks;

namespace DevInstinct.Patterns.UnitOfWorkPattern
{
    public interface IUnitOfWorkScope<TUnitOfWork> : IDisposable
        where TUnitOfWork : IUnitOfWork
    {
        TUnitOfWork UnitOfWork { get; set; }

        bool OwnsUnitOfWork { get; }

        bool ReadOnly { get; }

        /// <summary>
        /// Saves all changes made in this context to the underlying database.
        /// </summary>
        /// <returns>The number of objects written to the underlying database.</returns>
        void Commit();

        /// <summary>
        /// Asynchronously saves all changes made in this context to the underlying database.
        /// </summary>
        /// <returns>A task that represents the asynchronous save operation.  The task result contains the number of objects written to the underlying database.</returns>
        Task CommitAsync();

        /// <summary>
        /// Asynchronously saves all changes made in this context to the underlying database.
        /// </summary>
        /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous save operation.  The task result contains the number of objects written to the underlying database.</returns>
        Task CommitAsync(CancellationToken cancellationToken);
    }
}
