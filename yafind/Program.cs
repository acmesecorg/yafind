using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System.Collections;
using System.Reflection;
using System.Text;

namespace yafind
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //TODO: implement command line arguments package

            //For now argument 1 algorithm
            //argument2 hash:plain

            if (args.Length != 2)
            {
                ConsoleUtil.WriteError("Invalid arguments.");
                return;
            }

            //Compile the function in argument 1 and make it callable

            //We replace the . with + as an operator, making the function syntax c# compatible
            var source = args[0].Replace('.', '+');

            //Add the references to other assemblies that may be needed here, as well as any imports for the code itself
            var references = new List<string> { "System.Linq" };
            var imports = new List<string> { "System" };
            var scriptOptions = ScriptOptions.Default.WithReferences(references).WithImports(imports);

            //Example code to add references from internal types
            //var assembly = Assembly.GetAssembly(typeof(MyType));
            //scriptOptions = scriptOptions.AddReferences(assembly);

            //Create a function wrapper around the source provided
            var builder = new StringBuilder();
            builder.AppendLine("string Func1(string plain, string salt)");
            builder.AppendLine("{");
            builder.AppendLine($"return {source};");
            builder.AppendLine("}");
            builder.AppendLine("return Func1(Plain, Salt);");

            //Create and compile the script
            var script = CSharpScript.Create<string>(builder.ToString(), scriptOptions, typeof(Context));
            var compilation = script.GetCompilation();

            //Our context will contain the injected values, as well as our primitives as static functions
            //It implements the IDisposabel pattern so that we have a hook to clean up any resources inside the context
            using (var context = new Context())
            {
                var splits = args[1].Split(':');

                //Set arguments
                context.Plain = splits[2];
                context.Salt = splits[1];

                var scriptState = await script.RunAsync(context);

                //Compare return value as byte array with bytes for hash
                Console.WriteLine($"GOT: {scriptState.ReturnValue}");
            }
        }
    }
}