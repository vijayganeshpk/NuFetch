using System;
using System.Threading.Tasks;
using NLog;
using NuGet;

namespace NuFetch {
    internal class Program {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        private static void Main( string[] args ) {
            MainAsync( args ).Wait();
        }

        private static async Task MainAsync( string[] args ) {
            await Task.Run( () => {
                log.Trace( $"Entered MainAsync(string[] args='{string.Join( ", ", args )}')" );

                var appOptions = new ApplicationOption();
                var parseResult = CommandLine.Parser.Default.ParseArguments( args, appOptions );

                log.Trace( $"arguments parsing was successful? {parseResult}" );

                if ( !parseResult ) {
                    return;
                }

                log.Debug( $"Application options: {appOptions.ToJson()}" );

                var repo = PackageRepositoryFactory.Default.CreateRepository( appOptions.ServerSource );

                log.Trace( $"Connected to repository {repo.Source}" );

                var package = repo.FindPackage( appOptions.PackageId );

                log.Debug( $"Found package '{package.Id}'" );
                log.Debug( $"Version: {package.Version}" );
                log.Debug( $"Authors: {package.Authors}" );

                log.Trace( "Exiting MainAsync" );
            } );
        }
    }
}