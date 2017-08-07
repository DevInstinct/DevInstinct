using System;
using System.Security.Claims;

namespace DevInstinct.Patterns
{
    public interface IUserContext<T>
        where T : IEquatable<T>
    {
        T Id { get; }

        ClaimsPrincipal ClaimsPrincipal { get; }
    }
}
