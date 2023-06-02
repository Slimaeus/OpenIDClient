using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenID.Web.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace OpenID.Web.Controllers;
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    // Đánh dấu action cần xác thực và chương trình sẽ tự động chuyển đến trang đăng nhập Google
    [Authorize]
    public IActionResult UserInfo()
    {
        var claims = User.Claims;
        var userInfo = new Info(claims);
        return View(userInfo);
    }

    public class Info
    {
        public string Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Info(IEnumerable<Claim> claims)
        {
            Id = claims.SingleOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            Name = claims.SingleOrDefault(x => x.Type == ClaimTypes.GivenName).Value;
            Email = claims.SingleOrDefault(x => x.Type == ClaimTypes.Email).Value;
            Image = claims.SingleOrDefault(x => x.Type == "picture").Value;
        }
    }

    public IActionResult Logout()
    {
        HttpContext.SignOutAsync();
        return RedirectToAction("Index");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
