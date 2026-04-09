using System.Diagnostics;
using System.Text.Json;
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
    public async Task<IActionResult> LogAction([FromBody] LogActionRequest request)
    {
        if (request == null || string.IsNullOrEmpty(request.Text) || request.Text.Length > 100)
        {
            return Json(new { success = false, message = "Text is required and must be 100 characters or less." });
        }

        try
        {
            var result = await _n8nService.LogActionAsync(request.Text);
            var n8nResponse = JsonSerializer.Deserialize<N8nResponse>(result, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return Json(new
            {
                success = true,
                message = n8nResponse?.Message
            });
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
