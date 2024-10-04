using BouillonChanvre.Data;
using BouillonChanvre.Services; // For ProductService
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Conditional configuration for database based on environment
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
    // .EnableSensitiveDataLogging()
    // .EnableDetailedErrors()
    // .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information));
}
else
{
    var password = Environment.GetEnvironmentVariable("DB_PASSWORD");
    var rawConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    var connectionString = rawConnectionString.Replace("{Password}", password);

    builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString));
}

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ISubCategoryService, SubCategoryService>();

// Add Razor Pages and Blazor Server support
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();


