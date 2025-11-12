using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Extensions;

public static class ConfigureRoles
{
    public static async Task ConfigureIdentityRoles(this RoleManager<IdentityRole> roleManager)
    {
        if (!await roleManager.RoleExistsAsync(Roles.ADMIN))
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.ADMIN));
        }
        if (!await roleManager.RoleExistsAsync(Roles.MEMBER))
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.MEMBER));
        }
    }
}
