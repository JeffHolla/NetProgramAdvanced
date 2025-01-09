using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

// Postgres
var postgres = builder.AddPostgres("postgres")
                      .WithDataVolume("postgres-volume");
var postgresdb = postgres.AddDatabase("postgresdb");

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
                    .AddProject("catalogservice", ".\\CatalogService\\src\\Web\\Web.csproj")
                    .WithReference(cartService)
                    .WaitFor(cartService)
                    .WithReference(postgresdb)
                    .WaitFor(postgresdb);


builder.Build().Run();
