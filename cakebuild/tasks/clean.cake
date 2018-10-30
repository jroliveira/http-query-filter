Task("Clean")
    .Does(() => {
        CleanDirectory(artifactsDirectory);
        Information($"  Clean completed for directory \"{artifactsDirectory}\".");

        CleanDirectories("./../src/**/bin");
        Information($"  Clean completed for directory \"./../src/**/bin\".");

        CleanDirectories("./../src/**/obj");
        Information($"  Clean completed for directory \"./../src/**/obj\".");
    });