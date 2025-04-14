using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);





builder.Services.Configure<IdentityOptions>(option =>
{
    option.Password.RequiredLength = 9;
    /*  option.Password.RequiredLength = 12;*/
    option.Password.RequiredUniqueChars = 1;
    option.Password.RequireUppercase = true;
    option.Password.RequireLowercase = true;
    option.Password.RequireDigit = true;
    option.Password.RequireNonAlphanumeric = true;
    option.SignIn.RequireConfirmedEmail = true;
    option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(20);
    option.Lockout.MaxFailedAccessAttempts = 5;
});
builder.Services.ConfigureApplicationCookie(config =>
{
    config.LoginPath = "/Identity/Account/Login";
    config.AccessDeniedPath = "/Account/AccessDenied";
}
);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapHub<ChatHub>("/chathub");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
