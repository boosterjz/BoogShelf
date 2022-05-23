using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookShelf.Models;

public static class IdentitySeedData {
    private const string AdminUser = "Admin";
    private const string AdminPassword = "Secret123$";

    public static async void EnsurePopulated(IApplicationBuilder app) {

        var context = app.ApplicationServices
            .CreateScope().ServiceProvider
            .GetRequiredService<AppIdentityDbContext>();
        // if ((await context.Database.GetPendingMigrationsAsync()).Any()) {
        //     await context.Database.MigrateAsync();
        // }

        await context.Database.EnsureCreatedAsync();

        var userManager = app.ApplicationServices
            .CreateScope().ServiceProvider
            .GetRequiredService<UserManager<BookShelfUser>>();
        
        var roleManager = app.ApplicationServices
            .CreateScope().ServiceProvider
            .GetRequiredService<RoleManager<IdentityRole>>();

        var role = await roleManager.FindByNameAsync("admin");
        if (role == null)
        {
            role = new IdentityRole("admin");
            await roleManager.CreateAsync(role);
        }

        var user = await userManager.FindByNameAsync(AdminUser);
        if (user == null)
        {
            user = new BookShelfUser("Admin");
            await userManager.CreateAsync(user, AdminPassword);
            await userManager.AddToRoleAsync(user, "admin");
        }
    }
}