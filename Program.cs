using BouillonChanvre.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(); 

// Use SQL Server instead of PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Conditional configuration for HttpClient based on environment
if (builder.Environment.IsDevelopment())
{
    // Local development API base address
    builder.Services.AddHttpClient("LocalAPI", client =>
    {
        client.BaseAddress = new Uri("http://localhost:5094/");
    });
}
else
{
    // Production API base address
    builder.Services.AddHttpClient("LocalAPI", client =>
    {
        client.BaseAddress = new Uri("https://bouillonchanvre-emeud7bsbedsc2f8.westeurope-01.azurewebsites.net/");
    });
}

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("LocalAPI"));

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

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();


