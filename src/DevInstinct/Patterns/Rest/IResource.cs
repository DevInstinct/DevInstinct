using System;

namespace DevInstinct.Patterns.Rest
{
    public interface IResource
    {
    }

    public interface IResource<T> : IResource, IKey<T> where T : IEquatable<T>
    {
    }
}
