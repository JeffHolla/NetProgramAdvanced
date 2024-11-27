using CatalogService.Infrastructure.Security.Identity;
using IdentityModel;
using IdentityServer.Data;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace IdentityServer
{
    public static class InitialiserExtensions
    {
        public static async Task InitialiseDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

            await initialiser.InitialiseAsync();

            await initialiser.SeedAsync();
        }
    }

    public class ApplicationDbContextInitialiser
    {
        private readonly ILogger<ApplicationDbContextInitialiser> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ApplicationDbContextInitialiser(
            ILogger<ApplicationDbContextInitialiser> logger,
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                await _context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        public async Task TrySeedAsync()
        {
            // Seed roles
            // https://medium.com/@iceboy2009d/role-permissions-with-aspnetidentity-and-identityserver-b70cc7d47528
            var managerRole = new IdentityRole(ApplicationRoles.Manager);
            await SeedRoleInDatabaseAsync(managerRole, []);

            var customerRole = new IdentityRole(ApplicationRoles.StoreCustomer);
            await SeedRoleInDatabaseAsync(customerRole, []);


            // Seed test Manager user (has both storeCustomer and manager roles)            
            var managerClaims = new Claim[]
            {
                new(JwtClaimTypes.Name, "Bob Smith"),
                new(JwtClaimTypes.GivenName, "Bob"),
                new(JwtClaimTypes.FamilyName, "Smith"),
                new(JwtClaimTypes.WebSite, "http://bob.com"),
                new("location", "somewhere")
            };

            var managerUser = new ApplicationUser { UserName = "managerUser", Email = "manager@gmail.com", EmailConfirmed = true };
            await SeedUserInDatabaseAsync(managerUser, "someRandomPassword1!", [customerRole, managerRole], managerClaims);


            // Seed test Customer user (has only storeCustomer role)
            var customerClaims = new Claim[]
            {
                new(JwtClaimTypes.Name, "Alice Smith"),
                new(JwtClaimTypes.GivenName, "Alice"),
                new(JwtClaimTypes.FamilyName, "Smith")
            };

            var customerUser = new ApplicationUser { UserName = "customerUser", Email = "customer@gmail.com", EmailConfirmed = true };
            await SeedUserInDatabaseAsync(customerUser, "someRandomPassword2!", [customerRole], customerClaims);
        }

        private async Task SeedRoleInDatabaseAsync(IdentityRole role, IEnumerable<Claim> roleClaims)
        {
            var isRoleExists = await _roleManager.Roles.AnyAsync(dbRole => dbRole.Name == role.Name);
            if (!isRoleExists)
            {
                await _roleManager.CreateAsync(role);

                foreach (var claim in roleClaims)
                {
                    await _roleManager.AddClaimAsync(role, claim);
                }
            }
        }

        private async Task SeedUserInDatabaseAsync(ApplicationUser user, string password, IEnumerable<IdentityRole> roles, IEnumerable<Claim> claims)
        {
            var isUserExists = await _userManager.Users.AnyAsync(dbUser => dbUser.UserName == user.UserName);
            if (!isUserExists)
            {
                await _userManager.CreateAsync(user, password);

                var roleNames = roles.Select(role => role.Name);
                await _userManager.AddToRolesAsync(user, roleNames);

                await _userManager.AddClaimsAsync(user, claims);
            }
        }
    }
}
