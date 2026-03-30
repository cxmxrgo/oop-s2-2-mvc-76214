using FoodSafetyInspectionTracker.Constants;
using FoodSafetyInspectionTracker.Data;
using FoodSafetyInspectionTracker.Models;
using FoodSafetyInspectionTracker.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FoodSafetyInspectionTracker.Controllers;

[Authorize(Roles = $"{ApplicationRoles.Admin},{ApplicationRoles.Inspector},{ApplicationRoles.Viewer}")]
public class InspectionsController(ApplicationDbContext dbContext, IInspectionService inspectionService, ILogger<InspectionsController> logger) : Controller
{
    public async Task<IActionResult> Index()
    {
        var inspections = await dbContext.Inspections
            .AsNoTracking()
            .Include(i => i.Premises)
            .OrderByDescending(i => i.InspectionDate)
            .ToListAsync();
        return View(inspections);
    }

    [Authorize(Roles = $"{ApplicationRoles.Admin},{ApplicationRoles.Inspector}")]
    public async Task<IActionResult> Create()
    {
        await PopulatePremisesAsync();
        return View(new Inspection { InspectionDate = DateTime.UtcNow.Date });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = $"{ApplicationRoles.Admin},{ApplicationRoles.Inspector}")]
    public async Task<IActionResult> Create(Inspection inspection)
    {
        if (!ModelState.IsValid)
        {
            logger.LogWarning("Inspection create validation failed for premises {PremisesId}", inspection.PremisesId);
            await PopulatePremisesAsync();
            return View(inspection);
        }

        var result = await inspectionService.CreateAsync(inspection);
        if (!result.Success)
        {
            ModelState.AddModelError(string.Empty, result.Error ?? "Unable to create inspection.");
            await PopulatePremisesAsync();
            return View(inspection);
        }

        logger.LogInformation("Inspection create request successful for premises {PremisesId}", inspection.PremisesId);
        return RedirectToAction(nameof(Index));
    }

    private async Task PopulatePremisesAsync()
    {
        var premises = await dbContext.Premises.AsNoTracking().OrderBy(p => p.Name).ToListAsync();
        ViewBag.PremisesId = new SelectList(premises, nameof(Premises.Id), nameof(Premises.Name));
    }
}
