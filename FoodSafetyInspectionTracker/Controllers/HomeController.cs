using FoodSafetyInspectionTracker.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FoodSafetyInspectionTracker.Controllers
{
    public class HomeController(ILogger<HomeController> logger) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            if (exceptionFeature?.Error is not null)
            {
                logger.LogError(exceptionFeature.Error,
                    "Unhandled exception encountered. Path: {Path}, RequestId: {RequestId}",
                    exceptionFeature.Path,
                    Activity.Current?.Id ?? HttpContext.TraceIdentifier);
            }
            else
            {
                logger.LogError("Unhandled exception encountered without exception details. RequestId: {RequestId}",
                    Activity.Current?.Id ?? HttpContext.TraceIdentifier);
            }

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
