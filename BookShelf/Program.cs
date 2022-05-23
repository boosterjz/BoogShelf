using BookShelf.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<StoreDbContext>(opts =>
    opts.UseNpgsql(builder.Configuration.GetConnectionString("BookShelfConnection")));

builder.Services.AddScoped<IStoreRepository, EfStoreRepository>();
builder.Services.AddScoped<IOrderRepository, EfOrderRepository>();

builder.Services.AddDbContext<AppIdentityDbContext>(opts =>
    opts.UseNpgsql(builder.Configuration.GetConnectionString("IdentityConnection")));

builder.Services.AddIdentity<BookShelfUser, IdentityRole>()
    .AddEntityFrameworkStores<AppIdentityDbContext>();

builder.Services.AddRazorPages();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddScoped(SessionCart.GetCart);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddServerSideBlazor();


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
    "Categories/{category}/Page{productPage:int}",
    new { Controller = "Home", action = "Index" });

app.MapControllerRoute("genrepage",
    "Genres/{genre}/Page{productPage:int}",
    new { Controller = "Home", action = "Genre" });

app.MapControllerRoute("page",
    "Author/{author}/Page{productPage:int}",
    new { Controller = "Home", action = "author" });

app.MapControllerRoute("page", "Page{productPage:int}",
    new { Controller = "Home", action = "Index", productPage = 1 });

app.MapControllerRoute("category", "Categories/{category}",
    new { Controller = "Home", action = "Index", productPage = 1 });

app.MapControllerRoute("genres", "Genres/{category}",
    new { Controller = "Home", action = "Genre", productPage = 1 });

app.MapControllerRoute("authors", "Authors/{category}",
    new { Controller = "Home", action = "Author", productPage = 1 });

app.MapControllerRoute("pagination",
    "Products/Page{productPage}",
    new { Controller = "Home", action = "Index", productPage = 1 });

app.MapDefaultControllerRoute();
app.MapRazorPages();
app.MapBlazorHub();
app.MapFallbackToPage("/admin/{*catchall}", "/Admin/Index");

SeedData.EnsurePopulated(app);
IdentitySeedData.EnsurePopulated(app);

app.Run();
