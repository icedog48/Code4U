using Code4U.Helpers;
using RazorEngine.Compilation;
using RazorEngine.Compilation.ReferenceResolver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RazorEngine.Compilation.ReferenceResolver
{
    public class Code4UReferenceResolver : IReferenceResolver
    {
        private readonly string directory;

        public Code4UReferenceResolver(string directory)
        {
            this.directory = directory;

            this.directory = Path.Combine(this.directory, @"..\");

            this.directory = Path.GetFullPath(this.directory);
        }

        public string FindLoaded(IEnumerable<string> refs, string find)
        {
            return refs.First(r => r.EndsWith(System.IO.Path.DirectorySeparatorChar + find));
        }

        public IEnumerable<CompilerReference> GetReferences(TypeContext context, IEnumerable<CompilerReference> includeAssemblies)
        {
            var references = new List<CompilerReference>();

            var assemblyPaths = FileSystemHelper.GetFiles(this.directory, "*.dll");

            assemblyPaths = assemblyPaths.GroupBy(x => Path.GetFileName(x)).Select(x => x.First()).ToArray();

            // TypeContext gives you some context for the compilation (which templates, which namespaces and types)

            // You must make sure to include all libraries that are required!
            // Mono compiler does add more standard references than csc! 
            // If you want mono compatibility include ALL references here, including mscorlib!
            // If you include mscorlib here the compiler is called with /nostdlib.
            var loadedAssemblies = (new UseCurrentAssembliesReferenceResolver())
                                        .GetReferences(context, includeAssemblies)
                                        .Select(r => r.GetFile())
                                        .ToArray();

            references.Add(CompilerReference.From(FindLoaded(loadedAssemblies, "mscorlib.dll")));
            references.Add(CompilerReference.From(FindLoaded(loadedAssemblies, "System.dll")));
            references.Add(CompilerReference.From(FindLoaded(loadedAssemblies, "System.Core.dll")));
            references.AddRange(assemblyPaths.Select(x => CompilerReference.From(x)));

            return references;
        }
    }
}
