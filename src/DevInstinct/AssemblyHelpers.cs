using System;
using System.Diagnostics;
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
                        try
                        {
                            var assembly = Assembly.LoadFrom(file);
                            //var dependencies = assembly.GetReferencedAssemblies();
                            //foreach (var dependency in dependencies)
                            //{
                            //    LoadDependencies(dependency);
                            //}
                            return assembly;
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex.Message);
                            return null;
                        }
                    })
                    .Where(assembly => assembly != null).ToList();

            return missingAssemblies.ToArray();
        }

        public static void LoadDependencies(AssemblyName assemblyName)
        {
            if (AppDomain.CurrentDomain.GetAssemblies().All(a => a.GetName() != assemblyName))
            {
                Debug.WriteLine(assemblyName.Name);
                Assembly.Load(assemblyName);
            }
        }
    }
}
