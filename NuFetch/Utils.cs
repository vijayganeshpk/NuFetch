using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using NuGet;

namespace NuFetch {
    public static class Utils {
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
            finalFullPath.Clear();
            finalFullPath.Append( Path.Combine( AppDomain.CurrentDomain.BaseDirectory, folderPath ) );

            return finalFullPath.ToString();
        }

        public static async Task DownloadPackage( IPackage package, IPackageRepository repo, string targetFolder ) {}
    }
}