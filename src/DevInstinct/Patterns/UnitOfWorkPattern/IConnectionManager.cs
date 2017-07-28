using System;

namespace DevInstinct.Patterns.UnitOfWorkPattern
{
    public interface IConnectionManager : IDisposable
    {
        bool OwnsConnection { get; }
    }
}
