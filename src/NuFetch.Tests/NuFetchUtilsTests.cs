using System;
using System.IO;
using NuFetchLib;
using Xunit;

namespace NuFetch.Tests {

    public class NuFetchUtilsTests {
        [Theory]
        [InlineData( @"C:\Temp\NuFetch" )]
        [InlineData( @"D:\HelloWorld" )]
        public void GetFullFolderPath_Returns_AbsoultePath_For_AbsolutePath( string folderPath ) {
            var finalFolderPath = Utils.GetFullFolderPath(folderPath);

            Assert.Equal( folderPath, finalFolderPath );
        }

        [Theory]
        [InlineData(@"Packages")]
        [InlineData(@"MyFolder\PackFolder")]
        public void GetFullFolderPath_Returns_AbsoultePath_For_RelativePath( string folderPath ) {
            var finalFolderPath = Utils.GetFullFolderPath(folderPath);

            var expectedPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folderPath);

            Assert.Equal( expectedPath, finalFolderPath );
        }
    }
}
