Task("Test")
    .Does(() => DotNetCoreTest(
        solutionPath, 
        new DotNetCoreTestSettings()
        {
            Configuration = configuration,
            NoBuild = true,
            NoRestore = true,
        }));
