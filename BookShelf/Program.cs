using Microsoft.EntityFrameworkCore;
using BookShelf.Models;

using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BookShelfDbContext>(opts => 
    opts.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection")));

builder.Services.AddScoped<IStoreRepository, BookShelfRepository>();
builder.Services.AddScoped<IOrderRepository, BookShelfOrderRepository>();

builder.Services.AddRazorPages();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddServerSideBlazor();

builder.Services.AddDbContext<AppIdentityDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("IdentityConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppIdentityDbContext>();

var app = builder.Build();

if (app.Environment.IsProduction()) {
    app.UseExceptionHandler("/error");
}

app.UseRequestLocalization(opts => {
    opts.AddSupportedCultures("ru-RU")
    .AddSupportedUICultures("ru-RU")
    .SetDefaultCulture("ru-RU");
});

app.UseStaticFiles();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute("catpage",
    "{category}/Page{page:int}",
    new { Controller = "Home", action = "Index" });

app.MapControllerRoute("page", "Page{page:int}",
    new { Controller = "Home", action = "Index", page = 1 });

app.MapControllerRoute("category", "{category}",
    new { Controller = "Home", action = "Index", page = 1 });

app.MapControllerRoute("pagination",
    "Books/Page{page}",
    new { Controller = "Home", action = "Index", page = 1 });

app.MapDefaultControllerRoute();
app.MapRazorPages();
app.MapBlazorHub();
app.MapFallbackToPage("/admin/{*catchall}", "/Admin/Index");

SeedData.EnsurePopulated(app);
IdentitySeedData.EnsurePopulated(app);

app.Run();
