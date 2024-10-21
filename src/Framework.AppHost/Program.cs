var builder = DistributedApplication.CreateBuilder(args);

var redis = builder.AddRedis("redis");

builder.AddProject<Projects.SNET_Framework_Api>("snet-framework-api")
    .WithReference(redis);

builder.Build().Run();
