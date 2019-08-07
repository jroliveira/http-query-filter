using static System.String;

Task("Check-Release-Requirements")
    .Does<BuildData>(data =>
    {
        if (IsNullOrEmpty(data.PackageManager.GetApiKey()))
        {
            throw new NullReferenceException("Package manager API key cannot be null");
        }
    });
