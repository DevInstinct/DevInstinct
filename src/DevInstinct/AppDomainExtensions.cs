using System;
using System.Linq;
using System.Reflection;

namespace DevInstinct
{
    public static class AppDomainExtensions
    {
        public static Assembly[] GetProjectAssemblies(this AppDomain appDomain)
        {
            return appDomain.GetAssemblies()
                .Where(a => !a.IsDynamic && a.Location == typeof(AppDomainExtensions).GetTypeInfo().Assembly.Location)
                .ToArray();
        }
    }
}
