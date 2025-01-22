var builder = DistributedApplication.CreateBuilder(args);

// Postgres
var postgres = builder.AddPostgres("postgres")
                      .WithDataVolume("postgres-volume")
                      .AddDatabase("postgresdb");

// Keycloack 
var defaultLogin = builder.AddParameter("username", value: "admin");
var defaultPassword = builder.AddParameter("password", value:"admin");
var keycloak = builder.AddKeycloak("keycloak", 8080, defaultLogin, defaultPassword)
    .WithDataVolume("keycloak_volume");

// CartService
var cartService = builder
                    .AddProject<Projects.CartService>("cartservice")
                    .WithReference(keycloak)
                    .WaitFor(keycloak);

// CatalogService
var catalogService = builder
                    .AddProject<Projects.CatalogService>("catalogservice")
                    .WithReference(cartService)
                    .WaitFor(cartService)
                    .WithReference(keycloak)
                    .WaitFor(keycloak)
                    .WithReference(postgres)
                    .WaitFor(postgres);


builder.AddProject<Projects.OcelotGateway>("ocelotgateway");


builder.Build().Run();
