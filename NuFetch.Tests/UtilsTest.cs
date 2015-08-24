using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NuFetch.Tests {
    [TestClass]
    public class UtilsTest {
        [TestMethod]
        public void GetFullFolderPath_Returns_AbsoultePath_For_AbsolutePath() {
            var initialFolderPath = @"C:\Temp\NuFetch";
            var finalFolderPath = Utils.GetFullFolderPath(initialFolderPath);

            Assert.AreEqual( initialFolderPath, finalFolderPath );
        }
    }
}
