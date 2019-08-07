public sealed class BuildData
{
    public BuildData(
        ICakeContext context,
        string configuration,
        bool isSimulatingCI,
        Func<BuildData, Action<Exception>> errorHandler,
        RepositoryData repository,
        SolutionData solution,
        PackageManagerData packageManager)
    {
        this.Configuration = configuration;
        this.IsSimulatingCI = isSimulatingCI;
        this.Repository = repository;
        this.Solution = solution;
        this.PackageManager = packageManager;

        this.DirectoriesToDelete = context
            .GetDirectories(this.Solution.ArtifactsDirectory)
            .Concat(context.GetDirectories("./src/**/bin"))
            .Concat(context.GetDirectories("./src/**/obj"))
            .OrderBy(directory => directory.ToString())
            .ToList();

        this.ErrorHandler = errorHandler(this);
    }

    public string Configuration { get; }
    public bool IsSimulatingCI { get; }
    public Action<Exception> ErrorHandler { get; }
    public RepositoryData Repository { get; }
    public SolutionData Solution { get; }
    public PackageManagerData PackageManager { get; }
    public IEnumerable<DirectoryPath> DirectoriesToDelete { get; }
}

public sealed class RepositoryData
{
    public RepositoryData(string remote) => this.Remote = remote;

    public string Remote { get; }
}

public sealed class SolutionData
{
    public SolutionData(
        Func<string> getVersion,
        string artifactsDirectory,
        string slnPath,
        IReadOnlyCollection<string> projectsPath)
    {
        this.GetVersion = getVersion;
        this.ArtifactsDirectory = artifactsDirectory;
        this.SlnPath = slnPath;
        this.ProjectsPath = projectsPath;
    }

    public Func<string> GetVersion { get; }
    public string ArtifactsDirectory { get; }
    public string SlnPath { get; }
    public IReadOnlyCollection<string> ProjectsPath { get; }
}

public sealed class PackageManagerData
{
    public PackageManagerData(
        string source,
        Func<string> getApiKey,
        string toolPath,
        string packagesPath)
    {
        this.Source = source;
        this.GetApiKey = getApiKey;
        this.ToolPath = toolPath;
        this.PackagesPath = packagesPath;
    }

    public string Source { get; }
    public Func<string> GetApiKey { get; }
    public string ToolPath { get; }
    public string PackagesPath { get; }
}
