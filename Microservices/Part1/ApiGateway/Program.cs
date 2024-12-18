using ApiGateway.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = builder.Configuration["JWT:Issuer"]; 
        options.RequireHttpsMetadata = false;      
        options.Audience = builder.Configuration["JWT:Audience"];         
    });

builder.Services.AddAuthorization();
builder.Services.AddOcelot();
builder.Services.AddSingleton<CookieMiddleware>();

var app = builder.Build();

app.UseMiddleware<CookieMiddleware>();
app.UseOcelot().Wait();

app.UseHttpsRedirection();

app.Run();


