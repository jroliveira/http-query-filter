using static System.String;

readonly Func<string> GetConfiguration = () => Argument("configuration", "Release");

readonly Func<string> GetVersion = () => ParseJsonFromFile("./package.json")["version"].ToString();

readonly Func<string> GetNuGetApiKey = () => ParseJsonFromFile($"./build/ci-env{(IsSimulatingCI() ? ".dev" : "")}.json")["nuget"]["apiKey"].ToString();

readonly Func<string> GetNuGetSource = () => IsSimulatingCI()
    ? "http://localhost:9001/api/v2/package"
    : "https://www.nuget.org/api/v2/package";

readonly Func<string> GetRepositoryRemote = () => IsSimulatingCI()
    ? "mock"
    : "origin";

readonly Func<bool> IsSimulatingCI = () => HasArgument("simulating-ci");

readonly Func<BuildData, Action<Exception>> ErrorHandler = data => exception =>
{
    if (!data.IsSimulatingCI)
    {
        throw exception;
    }

    Warning(exception);
};

readonly Action<FilePath, ProcessArgumentBuilder, string> Execute = (FilePath fileName, ProcessArgumentBuilder arguments, string successMessage) =>
{
    Information($"> {fileName} {arguments.Render()}");
    using(var process = StartAndReturnProcess(fileName, new ProcessSettings { Arguments = arguments }))
	{
        process.WaitForExit();
        if (process.GetExitCode() == 0 && !IsNullOrEmpty(successMessage))
        {
            Information($"√ {successMessage}");
        }

        Information("");
	}
};
