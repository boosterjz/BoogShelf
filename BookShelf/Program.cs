using Microsoft.EntityFrameworkCore;
using BookShelf.Models;
using NLog;
using NLog.Web;

var logger = LogManager.Setup().GetCurrentClassLogger();
logger.Debug("Initializing main");

try {
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddControllersWithViews();
    builder.Services.AddDbContext<BookShelfDbContext>(optionsBuilder => 
        optionsBuilder
            .UseNpgsql(builder.Configuration.GetConnectionString("DbConnection")));
    builder.Services.AddScoped<IRepository, BookShelfRepository>();

    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    builder.Host.UseNLog();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseDeveloperExceptionPage();
    }

    app.UseStatusCodePages();
    app.UseStaticFiles();
    app.UseRouting();
    app.UseAuthorization();
    app.MapDefaultControllerRoute();

    app.Run();
} catch (Exception exc) {
    logger.Error(exc, "Stopped execution");
    throw;
} finally {
    LogManager.Shutdown();
}
