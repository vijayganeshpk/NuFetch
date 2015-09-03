using System.Threading.Tasks;
using CommandLine;
using NLog;
using NuFetchLib;

namespace NuFetch {
    internal class Program {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        private static void Main( string[] args ) {
            MainAsync( args ).Wait();
        }

        private static async Task MainAsync( string[] args ) {
            await Task.Run( () => {
                log.Trace( $"Entered MainAsync(string[] args='{string.Join( ", ", args )}')" );

                var appOptions = new NuFetchOption();
                var parseResult = Parser.Default.ParseArguments( args, appOptions );

                log.Trace( $"arguments parsing was successful? {parseResult}" );

                if( !parseResult ) {
                    return;
                }

                //log.Debug( $"Application options: {appOptions.ToJson()}" );

                Utils.GetPackageAndDependencies( appOptions );

                log.Trace( "Exiting MainAsync" );
            } );
        }
    }
}