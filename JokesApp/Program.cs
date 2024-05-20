using JokesApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Configuration.AddEnvironmentVariables();
builder.Configuration.AddCommandLine(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<JokesContext>(options =>
{
    options.UseSqlServer("Server=CARDOSOPC\\SQLEXPRESS;Database=JokesDB;Trusted_Connection=True;TrustServerCertificate=True;");
});

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(IdentityConstants.BearerScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySecretKey")), // Replace with a long, random key
        ValidateIssuer = true,
        ValidIssuer = "https://localhost:7047", // Replace with your issuer URL (e.g., your API's URL)
        ValidateAudience = true,
        ValidAudience = "https://localhost:7047", // Replace with your intended audience (usually the same as issuer)
    };
})
.AddBearerToken(IdentityConstants.BearerScheme);

builder.Services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<JokesContext>()
    .AddApiEndpoints();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapControllers();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapIdentityApi<User>();

app.Run();
