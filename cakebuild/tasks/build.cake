Task("Build")
    .Does(() => DotNetCoreBuild(
        solutionPath,
        new DotNetCoreBuildSettings()
        {
            Configuration = configuration,
            NoRestore = true,
            MSBuildSettings = new DotNetCoreMSBuildSettings
            {
                NoLogo = true,    
            },
        }));