using FleetManager.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace FleetManager.Data;

public static class DbSeeder
{
    public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        string[] roleNames = { "Admin", "Mechanic" };

        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
        var adminUser = await userManager.FindByEmailAsync("admin@fleet.com");
        if(adminUser != null)
        {
            if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
            var currentClaims = await userManager.GetClaimsAsync(adminUser);
            if (!currentClaims.Any(c => c.Type == "Permission" && c.Value == "DeleteVehicles"))
            {
                await userManager.AddClaimAsync(adminUser, new Claim("Permission", "DeleteVehicles"));
            }
        }
    }
}