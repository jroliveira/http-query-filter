Task("Generate-NuGet-Package")
    .DoesForEach<BuildData, string>(
        data => data.Solution.ProjectsPath,
        (data, projectPath, _) => DotNetCorePack(projectPath, new DotNetCorePackSettings()
        {
            Configuration = data.Configuration,
            NoBuild = true,
            NoRestore = true,
            OutputDirectory = data.Solution.ArtifactsDirectory,
            VersionSuffix = data.Solution.GetVersion(),
            MSBuildSettings = new DotNetCoreMSBuildSettings
            {
                NoLogo = true,
            },
        }));
