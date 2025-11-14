
var builder = DistributedApplication.CreateBuilder(args);
var api = builder.AddProject<Projects.Api>("api");

var postgres = builder.AddPostgres("pgsql", port: 5432)
    .WithDataVolume()
    .WithPgAdmin()
    .AddDatabase("AppDb");

var mongo = builder.AddMongoDB("mongo")
    .WithDataVolume()
    .WithMongoExpress();

var rabbit = builder.AddRabbitMQ("rabbitmq", port: 5672);

var redis = builder.AddValkey("cache", port: 6379)
    .WithDataVolume();

api
    .WithReference(postgres)
    .WaitFor(postgres)
    .WithReference(mongo)
    .WaitFor(mongo)
    .WithReference(rabbit)
    .WaitFor(rabbit)
    .WithReference(redis)
    .WaitFor(redis);

builder.Build().Run();
