using CommandLine;
using CommandLine.Text;

namespace NuFetchLib {
    public class NuFetchOption {
        [Option('s',"source", DefaultValue = "https://www.nuget.org/api/v2/", HelpText = "NuGet repository source server URL")]
        public string ServerSource { get; set; }

        [Option('p',"pid", Required = true, HelpText = "Package Id to download" )]
        public string PackageId { get; set; }

        [Option('v',"version", DefaultValue = null, HelpText = "Package Version to download; do not mention this parameter to donwload the latest version")]
        public string PackageVersion { get; set; }

        [Option("depVerType", DefaultValue = DependencyVersionTypeToDownload.Min, HelpText = "Dependency version to download (Min or Max version)")]
        public DependencyVersionTypeToDownload VersionTypeToDownload { get; set; }

        [Option('t',"targetFolder",DefaultValue = "Packages", HelpText = "Target folder path to download the packages")]
        public string TargetFolder { get; set; }

        [Option('o',"overwrite", DefaultValue = false, HelpText = "Flag to overwrite existing file matching a package version")]
        public bool OverwriteExistingFiles { get; set; }

        [Option('i',"includePrelease", DefaultValue = false, HelpText = "Flag denoting if the downloader should include prerelease packages when searching for package")]
        public bool IncludePreRelease { get; set; }

        [Option('a',"allowUnlisted", DefaultValue = false, HelpText = "Flag denoting if the downloader should search for package in unlisted packages")]
        public bool AllowUnlisted { get; set; }

        [HelpOption]
        public string GetUsage() {
            return HelpText.AutoBuild( this,
                                       ( HelpText current ) => HelpText.DefaultParsingErrorsHandler( this, current ) );
        }
    }
}
