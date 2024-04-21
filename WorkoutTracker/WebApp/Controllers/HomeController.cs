using System.Diagnostics;
using App.DAL.EF;
using Base.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.ViewModels;

namespace WebApp.Controllers;

/// <summary>
/// 
/// </summary>
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="context"></param>
    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public IActionResult Index()
    {
        return View();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> Privacy()
    {
        var vm = new HomePrivacyVM();

        if (User.Identity == null)
        {
            return View();
        }

        vm.AppUser = await _context.Users
            .Include(u => u.AppRefreshTokens)
            .FirstOrDefaultAsync(u => u.Id == User.GetUserId());

        vm.AppUserClaims = await _context.UserClaims
            .Where(u => u.UserId == User.GetUserId())
            .ToListAsync();

        return View(vm);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}