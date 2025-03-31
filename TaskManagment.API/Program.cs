using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagment.Applications.Services;
using TaskManagment.Infrastructure.Authentication;
using TaskManagment.Infrastructure.Configuration;
using TaskManagment.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddOpenApi();

//подключение к бд
var connString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseNpgsql(connString));

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins, policy =>
    {
        policy.WithOrigins("https://localhost:7015") // Должен совпадать с клиентом!
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // ВАЖНО: разрешаем куки
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.Use(async (context, next) =>
{
    var cookie = context.Request.Headers["Cookie"].ToString();
    Console.WriteLine($"?? Cookie в запросе: {cookie}");
    await next();
});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();


app.MapControllers();

app.Map("/test", () => "Give me the loot");
app.Map("/data", [Authorize] () => new { message = "Happy Hacking!" });

app.MapFallbackToFile("index.html");

app.Run();

