using BouillonChanvre.Data;
using BouillonChanvre.Services;
using MudBlazor.Services;
using Microsoft.EntityFrameworkCore;
using Azure.Storage.Blobs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Conditional configuration for database based on environment
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
    var azuriteConnectionString = "AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;DefaultEndpointsProtocol=http;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;";

    builder.Services.AddSingleton<IBlobStorageService>(new BlobStorageService(azuriteConnectionString));
}
else
{
    var password = Environment.GetEnvironmentVariable("DB_PASSWORD");
    var rawConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    var connectionString = rawConnectionString.Replace("{Password}", password);

    builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString));

    string blobConnectionString = Environment.GetEnvironmentVariable("AZURE_BLOB_CONNECTION_STRING");

    if (string.IsNullOrEmpty(blobConnectionString))
    {
        throw new InvalidOperationException("Azure Blob Storage connection string is not set.");
    }

    builder.Services.AddSingleton<IBlobStorageService>(new BlobStorageService(blobConnectionString));
}

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ISubCategoryService, SubCategoryService>();

// Add Razor Pages and Blazor Server support
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddMudServices();

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


