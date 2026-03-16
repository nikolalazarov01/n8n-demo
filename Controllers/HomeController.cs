using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.Services;

namespace Project.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly N8nService _n8nService;

    public HomeController(ILogger<HomeController> logger, N8nService n8nService)
    {
        _logger = logger;
        _n8nService = n8nService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> LogAction()
    {
        try
        {
            var result = await _n8nService.LogActionAsync();
            return Json(new { success = true, message = result });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error calling n8n workflow");
            return Json(new { success = false, message = "An error occurred while logging the action." });
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
