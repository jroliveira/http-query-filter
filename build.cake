#addin "Cake.Docker&version=0.9.7"
#addin "Cake.Figlet&version=1.2.0"
#addin "Cake.Git&version=0.19.0"
#addin "Cake.Json&version=4.0.0"
#addin "Cake.Npm&version=0.17.0"

#load "build/*.cake"

Setup<BuildData>(context =>
{
    Information(Figlet("Http.Query.Filter"));

    return new BuildData(
        context,
        GetConfiguration(),
        IsSimulatingCI(),
        ErrorHandler,
        new RepositoryData(
            GetRepositoryRemote()),
        new SolutionData(
            GetVersion,
            "./artifacts",
            "./Http.Query.Filter.sln",
            new[]
            {
                "./src/Http.Query.Filter",
                "./src/Http.Query.Filter.Client",
            }),
        new PackageManagerData(
            GetNuGetSource(),
            GetNuGetApiKey,
            ".nuget/nuget.exe",
            "./artifacts/*.nupkg"));
});

Task("Default")
    .IsDependentOn("Restore-NuGet-Packages")
    .IsDependentOn("Build-Solution")
    .IsDependentOn("Run-Tests");

Task("Release")
    .IsDependentOn("Check-Release-Requirements")
    .IsDependentOn("Mock-CI-Environment")
    .IsDependentOn("Delete-Temp-Directories")
    .IsDependentOn("Restore-NuGet-Packages")
    .IsDependentOn("Build-Solution")
    .IsDependentOn("Run-Tests")
    .IsDependentOn("Generate-Changelog")
    .IsDependentOn("Generate-NuGet-Package")
    .IsDependentOn("Push-NuGet-Package")
    .IsDependentOn("Push-Git-Tag-And-Changes")
    .IsDependentOn("Destroy-CI-Environment");

RunTarget(Argument("target", "Default"));
