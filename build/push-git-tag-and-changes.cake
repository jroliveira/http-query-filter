Task("Push-Git-Tag-And-Changes")
    .Does<BuildData>(data => Execute(
        "git",
        $"push {data.Repository.Remote} --follow-tags",
        $"pushing changes and tags to git remote {data.Repository.Remote}"))
    .OnError<BuildData>((exception, data) => data.ErrorHandler(exception));
