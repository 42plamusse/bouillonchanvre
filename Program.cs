using BouillonChanvre.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(); 

// Conditional configuration for HttpClient based on environment
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
    // Local development API base address
    builder.Services.AddHttpClient("LocalAPI", client =>
    {
        client.BaseAddress = new Uri("http://localhost:5094/");
    });
}
else
{
    var password = Environment.GetEnvironmentVariable("DB_PASSWORD");

    // Replace the password placeholder in the connection string
    var rawConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    var connectionString = rawConnectionString.Replace("{Password}", password);

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString));
        
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


