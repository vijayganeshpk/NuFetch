using System;
using System.IO;
using System.Text;

namespace NuFetch {
    public static class Utils {
        public static string GetFullFolderPath( string folderPath ) {
            var finalFullPath = new StringBuilder(folderPath);

            if( !Path.IsPathRooted( folderPath ) ) { // we have a relative path
                finalFullPath.Clear();
                finalFullPath.Append( Path.Combine( AppDomain.CurrentDomain.BaseDirectory, folderPath ) );
            }

            return finalFullPath.ToString();
        }
    }
}