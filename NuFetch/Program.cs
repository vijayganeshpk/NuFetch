using System;
using System.Threading.Tasks;
using NLog;

namespace NuFetch {
    class Program {
        readonly static Logger log = LogManager.GetCurrentClassLogger();
        static void Main( string[] args ) {
            MainAsync( args ).Wait();
        }

        static async Task MainAsync(string[] args) {
            await Task.Run( () => {
                log.Trace( $"Entered MainAsync(string[] args='{string.Join(", ", args )}')" );

                var appOptions = new ApplicationOption();
                var parseResult = CommandLine.Parser.Default.ParseArguments(args, appOptions);

                log.Trace( $"arguments parsing was successful? {parseResult}" );

                if( !parseResult ) {
                    return;
                }

                log.Debug( $"Application options: {appOptions.ToJson()}" );

                log.Trace( "Exiting MainAsync" );
            } );
        }
    }
}
