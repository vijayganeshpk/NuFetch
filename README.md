# NuFetch
NuGet packages with dependency downloader

Usage:

`NuFetch -p <packageid> [-s <sourceserver>] [-v <packageversion>] [-t <targetfolder>] [-o] [-i] [-a]`

```
 -s, --source             (Default: https://www.nuget.org/api/v2/) NuGet
                           repository source server URL

  -p, --pid                Required. Package Id that needs to be downloaded
                           with dependecies

  -v, --version            (Default: ) Package Version to download; do not
                           mention this parameter to donwload the latest version

  --depVerType             (Default: Min) Dependency version to download (Min
                           or Max version)

  -t, --targetFolder       (Default: Packages) Target folder path to download
                           the packages

  -o, --overwrite          (Default: False) Flag to overwrite existing file
                           matching a package version

  -i, --includePrelease    (Default: False) Flag denoting if the downloader
                           should include prereleases when searching for package

  -a, --allowUnlisted      (Default: False) Flag denoting if the downloader
                           should search for package in unlisted packages

  --help                   Display this help screen.
  ```
