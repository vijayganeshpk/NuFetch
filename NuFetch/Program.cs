using System.Threading.Tasks;

namespace NuFetch {
    class Program {
        static void Main( string[] args ) {
            MainAsync( args ).Wait();
        }

        static async Task MainAsync(string[] args) {
            await Task.Run( () => {
                var appOptions = new ApplicationOption();
                var parseResult = CommandLine.Parser.Default.ParseArguments(args, appOptions);

                if( !parseResult ) {
                    return;
                }
            } );
        }
    }
}
