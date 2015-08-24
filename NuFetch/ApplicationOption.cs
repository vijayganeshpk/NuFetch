using CommandLine;
using CommandLine.Text;

namespace NuFetch {
    class ApplicationOption {
        [Option('s',"source", DefaultValue = "https://www.nuget.org/api/v2/", HelpText = "NuGet repository source server URL")]
        public string ServerSource { get; set; }

        [Option('p',"pid", Required = true, HelpText = "Package Id that needs to be downloaded with dependecies")]
        public string PackageId { get; set; }

        [Option('t',"targetFolder",DefaultValue = "Packages", HelpText = "Target folder path to download the packages")]
        public string TargetFolder { get; set; }

        [HelpOption]
        public string GetUsage() {
            return HelpText.AutoBuild( this,
              ( HelpText current ) => HelpText.DefaultParsingErrorsHandler( this, current ) );
        }
    }
}
