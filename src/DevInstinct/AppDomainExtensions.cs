using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DevInstinct
{
    public static class AppDomainExtensions
    {
        public static Assembly[] GetProjectAssemblies(this AppDomain appDomain)
        {
            var baseDirectory = Path.GetDirectoryName(typeof(AppDomainExtensions).GetTypeInfo().Assembly.Location).ToLower();
            return appDomain.GetAssemblies()
                .Where(a => !a.IsDynamic && Path.GetDirectoryName(a.Location).ToLower() == baseDirectory)
                .ToArray();
        }
    }
}
