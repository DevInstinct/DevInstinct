using System.Collections.Generic;

namespace System.Linq
{
    // https://mfelicio.com/2010/01/13/c-tips-ienumerablet-foreach-extension-method/
    public static class EnumerableExtensionMethods
    {
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            foreach (var item in source)
            {
                action(item);
            }

            return source;
        }
    }
}

