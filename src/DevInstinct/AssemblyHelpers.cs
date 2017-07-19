using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DevInstinct
{
    public static class AssemblyHelpers
    {
        public static Assembly[] LoadPlugAndPlayAssemblies()
        {
            // TODO: in .NET Core, EF uses DI for the context - will need to find a way to abstract that initialization.
            // TODO: generic code should also search for *.exe and use file filters for both the directory and the loaded assemblies.
            var candidateAssemblyFiles =
                new FileInfo(new Uri(typeof(AssemblyHelpers).GetTypeInfo().Assembly.CodeBase).LocalPath).Directory.GetFiles("*.dll")
                    .Select(f => f.FullName.ToLower()).ToList();

            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            var loadedAssemblyFiles = loadedAssemblies.Where(a => !a.IsDynamic).Select(a => new Uri(a.CodeBase).LocalPath.ToLower()).ToList();

            var missingAssemblyFiles = candidateAssemblyFiles.Except(loadedAssemblyFiles);
            var missingAssemblies =
                missingAssemblyFiles
                    .Select(file =>
                    {
                        try { return Assembly.LoadFile(file); }
                        catch { return null; }
                    })
                    .Where(assembly => assembly != null).ToList();

            return missingAssemblies.ToArray();
        }
    }
}
