using FoodSafetyInspectionTracker.Constants;
using FoodSafetyInspectionTracker.Models;
using FoodSafetyInspectionTracker.Models.ViewModels;
using FoodSafetyInspectionTracker.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodSafetyInspectionTracker.Controllers;

[Authorize(Roles = $"{ApplicationRoles.Admin},{ApplicationRoles.Inspector},{ApplicationRoles.Viewer}")]
public class DashboardController(IDashboardService dashboardService, ILogger<DashboardController> logger) : Controller
{
    public async Task<IActionResult> Index(string? town, RiskRating? riskRating)
    {
        var vm = await dashboardService.GetDashboardAsync(new DashboardFilter { Town = town, RiskRating = riskRating }, DateTime.UtcNow);
        logger.LogInformation("Dashboard viewed by {UserName}. Town: {Town}, RiskRating: {RiskRating}", User.Identity?.Name ?? "Anonymous", town, riskRating);
        return View(vm);
    }
}
