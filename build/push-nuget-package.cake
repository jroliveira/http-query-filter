Task("Push-NuGet-Package")
    .Does<BuildData>(data => NuGetPush(GetFiles(data.PackageManager.PackagesPath), new NuGetPushSettings
    {
        Source = data.PackageManager.Source,
        ApiKey = data.PackageManager.GetApiKey(),
        ToolPath = data.PackageManager.ToolPath,
    }))
    .OnError<BuildData>((exception, data) => data.ErrorHandler(exception));
