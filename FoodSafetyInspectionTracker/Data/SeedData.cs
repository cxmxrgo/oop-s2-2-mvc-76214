using FoodSafetyInspectionTracker.Constants;
using Microsoft.AspNetCore.Identity;

namespace FoodSafetyInspectionTracker.Data;

public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider services, ILogger logger)
    {
        using var scope = services.CreateScope();
        var scopedServices = scope.ServiceProvider;

        var context = scopedServices.GetRequiredService<ApplicationDbContext>();
        context.Database.EnsureCreated();

        var roleManager = scopedServices.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scopedServices.GetRequiredService<UserManager<IdentityUser>>();

        await EnsureRoleAsync(roleManager, ApplicationRoles.Admin, logger);
        await EnsureRoleAsync(roleManager, ApplicationRoles.Inspector, logger);
        await EnsureRoleAsync(roleManager, ApplicationRoles.Viewer, logger);

        await EnsureUserAsync(userManager, "admin@foodsafety.local", "Passw0rd!", ApplicationRoles.Admin, logger);
        await EnsureUserAsync(userManager, "inspector@foodsafety.local", "Passw0rd!", ApplicationRoles.Inspector, logger);
        await EnsureUserAsync(userManager, "viewer@foodsafety.local", "Passw0rd!", ApplicationRoles.Viewer, logger);
    }

    private static async Task EnsureRoleAsync(RoleManager<IdentityRole> roleManager, string role, ILogger logger)
    {
        if (await roleManager.RoleExistsAsync(role))
        {
            return;
        }

        await roleManager.CreateAsync(new IdentityRole(role));
        logger.LogInformation("Role seeded: {Role}", role);
    }

    private static async Task EnsureUserAsync(UserManager<IdentityUser> userManager, string email, string password, string role, ILogger logger)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user is null)
        {
            user = new IdentityUser { UserName = email, Email = email, EmailConfirmed = true };
            var createResult = await userManager.CreateAsync(user, password);
            if (!createResult.Succeeded)
            {
                logger.LogWarning("Unable to seed user {Email}: {Errors}", email, string.Join(";", createResult.Errors.Select(e => e.Description)));
                return;
            }

            logger.LogInformation("User seeded: {Email}", email);
        }

        if (!await userManager.IsInRoleAsync(user, role))
        {
            await userManager.AddToRoleAsync(user, role);
            logger.LogInformation("Role {Role} assigned to user {Email}", role, email);
        }
    }
}
