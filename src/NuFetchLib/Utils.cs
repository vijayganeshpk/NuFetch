using System;
using System.IO;
using System.Linq;
using System.Text;
using NLog;
using NuGet;

namespace NuFetchLib {
    public static class Utils {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// Method for getting the abosulte path
        /// </summary>
        /// <param name="folderPath">Folder path, can be relative or absolute</param>
        /// <returns>The determined absolute path from the input parameter</returns>
        public static string GetFullFolderPath( string folderPath ) {
            if ( folderPath == null ) {
                throw new ArgumentNullException( nameof( folderPath ) );
            }

            var finalFullPath = new StringBuilder( folderPath );

            // no need to do anything for absolute paths
            if ( Path.IsPathRooted( folderPath ) ) {
                return finalFullPath.ToString();
            }

            // convert relative paths to absolute path
            finalFullPath.Remove( 0, finalFullPath.Length );
            finalFullPath.Append( Path.Combine( AppDomain.CurrentDomain.BaseDirectory, folderPath ) );

            return finalFullPath.ToString();
        }

        public static void GetPackageAndDependencies( string packageId, string packageVersion, string sourceServer, string targetFolder, bool overwriteExistingFiles, bool includePrerelease, bool allowUnlisted, DependencyVersionTypeToDownload depVersionToDownload) {

            var repo = PackageRepositoryFactory.Default.CreateRepository( sourceServer );
            var package = repo.FindPackage( packageId, packageVersion==null?null:new SemanticVersion(packageVersion),NullConstraintProvider.Instance, includePrerelease, allowUnlisted ) as DataServicePackage;

            if( package == null ) {
                log.Warn( $"Package '{packageId}' could not be found in the repository '{sourceServer}', or it could be converted as DataServicePackage" );
                return;
            }

            var finalPackagePath = Path.Combine( targetFolder, $"{package.Id}.{package.Version}.nupkg" );

            if( File.Exists( finalPackagePath ) && !overwriteExistingFiles ) {
                log.Debug( $"File '{finalPackagePath}' already exists, skipping it" );
                return;
            }

            if( !Directory.Exists( targetFolder ) ) {
                Directory.CreateDirectory( targetFolder );
            }

            using( var fs = File.Open( finalPackagePath, FileMode.Create ) ) {
                log.Debug($"Downloading package '{package.Id}' from '{package.DownloadUrl}' ... ");
                var downloader = new PackageDownloader();
                downloader.DownloadPackage( package.DownloadUrl, package, fs );
                log.Debug($"... done downloading package {package.Id}");
            }

            foreach( var dset in package.DependencySets.Where( dset => dset.Dependencies.Count > 0 ) ) {
                log.Debug( $"Downloading dependencies for the dependency set: {dset.TargetFramework?.ToString() ?? "<default set>"} " );

                foreach( var dep in dset.Dependencies ) {
                    log.Debug( $"Processing dependency '{dep.Id}'" );
                    var dependencyVersion = depVersionToDownload == DependencyVersionTypeToDownload.Max
                                                ? dep.VersionSpec?.MaxVersion?.ToString()
                                                : dep.VersionSpec?.MinVersion?.ToString();
                    GetPackageAndDependencies( dep.Id, dependencyVersion, sourceServer, targetFolder, overwriteExistingFiles, includePrerelease, allowUnlisted, depVersionToDownload );
                }
            }
        }
    }
}