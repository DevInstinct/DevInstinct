using System;

namespace DevInstinct.Patterns
{
    public interface IKey<T>
        where T : IEquatable<T>
    {
        T Id { get; set; }
    }
}
