using DevInstinct.Patterns.UnitOfWorkPattern;
using System.Data;

namespace DevInstinct.EntityFramework.Patterns.UnitOfWorkPattern
{
    public interface IDbConnectionManager : IConnectionManager
    {
        IDbConnection Connection { get; }
    }
}
