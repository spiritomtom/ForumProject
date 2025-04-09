using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ForumProject.Data;
using ForumProject.Models;
using ForumProject.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<SentimentService>();

var app = builder.Build();

// Seed Roles
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await SeedRoles(roleManager);
}

async Task SeedRoles(RoleManager<IdentityRole> roleManager)
{
    string[] roles = { "Admin", "Moderator", "User" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

app.UseAuthentication();
app.UseAuthorization();
app.MapDefaultControllerRoute();
app.Run();
