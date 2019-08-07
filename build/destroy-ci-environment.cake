Task("Destroy-CI-Environment")
    .WithCriteria<BuildData>((context, data) => data.IsSimulatingCI)
    .Does<BuildData>(data =>
    {
        Execute(
            "git",
            $"remote remove {data.Repository.Remote}",
            $"removing git remote {data.Repository.Remote}");

        Execute(
            "git",
            $"tag --delete {data.Solution.GetVersion()}",
            "");

        Execute(
            "git",
            "reset --soft HEAD~1",
            "reseting last commit");

        Execute(
            "git",
            "reset .",
            "");

        DockerComposeDown(new DockerComposeDownSettings
        {
            Files = new [] { "docker-compose-ci.yml" },
        });
    });
