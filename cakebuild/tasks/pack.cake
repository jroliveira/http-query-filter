Task("Pack")
    .Does(() => {
        var settings = new DotNetCorePackSettings()
        {
            Configuration = configuration,
            NoBuild = true,
            NoRestore = true,
            OutputDirectory = artifactsDirectory,
            MSBuildSettings = new DotNetCoreMSBuildSettings
            {
                NoLogo = true,    
            },
        };

        DotNetCorePack("./../src/Http.Query.Filter/Http.Query.Filter.csproj", settings);
        DotNetCorePack("./../src/Http.Query.Filter.Client/Http.Query.Filter.Client.csproj", settings);
    });