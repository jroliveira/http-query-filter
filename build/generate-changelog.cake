Task("Generate-Changelog")
    .Does<BuildData>(data => NpmRunScript("release", new[]
    {
        "--release-as=patch",
        "--tag-prefix=",
        "--no-verify",
    }))
    .OnError<BuildData>((exception, data) => data.ErrorHandler(exception));
