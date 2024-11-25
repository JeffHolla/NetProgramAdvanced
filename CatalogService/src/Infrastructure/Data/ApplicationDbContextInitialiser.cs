using CatalogService.Domain.Entities;
using CatalogService.Infrastructure.Security.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CatalogService.Infrastructure.Data;

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
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ApplicationDbContextInitialiser(
        ILogger<ApplicationDbContextInitialiser> logger,
        ApplicationDbContext context,
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager)
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
        // Default manager role and user
        var managerRole = new IdentityRole(ApplicationRoles.Manager);
        await SeedRoleInDatabaseAsync(managerRole);

        var managerUser = new IdentityUser { UserName = "managerUser", Email = "manager@gmail.com" };
        await SeedUserInDatabaseAsync(managerUser, "someRandomPassword1!", managerRole);

        // Test customer role and user
        var customerRole = new IdentityRole(ApplicationRoles.StoreCustomer);
        await SeedRoleInDatabaseAsync(customerRole);

        var customerUser = new IdentityUser { UserName = "customerUser", Email = "customer@gmail.com" };
        await SeedUserInDatabaseAsync(customerUser, "someRandomPassword2!", customerRole);

        // Default test data
        if (!_context.Categories.Any())
        {
            var firstCategory = new Category
            {
                Name = "The First Category"
            };
            _context.Categories.Add(firstCategory);

            await _context.SaveChangesAsync();
        }
    }

    private async Task SeedRoleInDatabaseAsync(IdentityRole role)
    {
        var isRoleExists = await _roleManager.Roles.AnyAsync(dbRole => dbRole.Name == role.Name);
        if (!isRoleExists)
        {
            await _roleManager.CreateAsync(role);
        }
    }

    private async Task SeedUserInDatabaseAsync(IdentityUser user, string password, IdentityRole role)
    {
        var isUserExists = await _userManager.Users.AnyAsync(dbUser => dbUser.UserName == user.UserName);
        if (!isUserExists)
        {
            await _userManager.CreateAsync(user, password);
            if (!string.IsNullOrWhiteSpace(role.Name))
            {
                await _userManager.AddToRolesAsync(user, [role.Name]);
            }
        }
    }
}
