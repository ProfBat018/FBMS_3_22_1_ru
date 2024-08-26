using ApiFirst.Services.Classes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RazorAreas.Areas.Identity.Data.DbContexts;
using RazorAreas.Areas.Identity.Data.Models;



var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Default") ?? throw new InvalidOperationException("Connection string 'Default' not found.");

builder.Services.AddDbContext<UserContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddAuthorization(ops =>
{
    ops.AddPolicy("RequireSuperAdmin", p => p.RequireRole(AppRoles.SuperAdminAppUser));
    ops.AddPolicy("RequireAdmin", p => p.RequireRole(AppRoles.AdminAppUser));

});

builder.Services.AddIdentity<AppUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
      .AddDefaultTokenProviders()
        .AddDefaultUI()
                .AddEntityFrameworkStores<UserContext>();


builder.Services.AddRazorPages(ops =>
{
    ops.Conventions.AuthorizeFolder("/Admin", "RequireAdmin");
});

builder.Services.AddSingleton<IEmailSender, EmailSender>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
