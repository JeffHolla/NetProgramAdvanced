using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

//var postgres = builder
//                    .AddDockerfile("mypostgresql", "C:\\Users\\Aleksandr_Goriachkin\\Desktop\\NetProgramAdvanced\\docker_tests\\PostgreSQL");

var postgres = builder.AddPostgres("postgres")
                      .WithDataVolume("postgres-volume");
var postgresdb = postgres.AddDatabase("postgresdb");

//var identityService = builder
//                        .AddProject<Projects.IdentityServer>("identityserver")
//                        .WithExternalHttpEndpoints();


var keycloak = builder.AddKeycloak("keycloak", 8080);


var cartService = builder
                    .AddProject<Projects.CartService>("cartservice")
                    .WithExternalHttpEndpoints()
                    //.WithHttpEndpoint
                    .WithReference(postgresdb)
                    .WaitFor(postgresdb)
                    .WithReference(keycloak)
                    .WaitFor(keycloak);
                    //.WithReference(identityService);

//identityService.WithReference(cartService);


builder.Build().Run();
