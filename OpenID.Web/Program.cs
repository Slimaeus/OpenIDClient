using Google.Apis.Auth.AspNetCore3;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Thêm các dịch vụ của Xác thực, Cookie và Google OpenID Connect và config
// ************************************************************************
var configuration = builder.Configuration;
builder.Services.AddAuthentication(o =>
{
    o.DefaultChallengeScheme = GoogleOpenIdConnectDefaults.AuthenticationScheme;
    o.DefaultForbidScheme = GoogleOpenIdConnectDefaults.AuthenticationScheme;
    o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
        .AddCookie()
        .AddGoogleOpenIdConnect(options =>
        {
            options.ClientId = configuration["OpenID:Google:ClientId"];
            options.ClientSecret = configuration["OpenID:Google:ClientSecret"];
        });
// ************************************************************************

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Đăng ký các middleware xác thực
// ************************************************************************
app.UseAuthentication();
// ************************************************************************

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
