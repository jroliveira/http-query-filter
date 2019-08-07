Task("Mock-CI-Environment")
    .WithCriteria<BuildData>((context, data) => data.IsSimulatingCI)
    .Does<BuildData>(data =>
    {
        Execute(
            "git",
            $"remote add {data.Repository.Remote} localhost",
            $"adding git remote {data.Repository.Remote}");

        DockerComposeUp(new DockerComposeUpSettings
        {
            Files = new [] { "docker-compose-ci.yml" },
            Build = true,
            DetachedMode = true,
            ForceRecreate = true,
        });
    });
