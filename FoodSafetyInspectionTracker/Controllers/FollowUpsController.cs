using FoodSafetyInspectionTracker.Constants;
using FoodSafetyInspectionTracker.Data;
using FoodSafetyInspectionTracker.Models;
using FoodSafetyInspectionTracker.Models.ViewModels;
using FoodSafetyInspectionTracker.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FoodSafetyInspectionTracker.Controllers;

[Authorize(Roles = $"{ApplicationRoles.Admin},{ApplicationRoles.Inspector},{ApplicationRoles.Viewer}")]
public class FollowUpsController(ApplicationDbContext dbContext, IFollowUpService followUpService, ILogger<FollowUpsController> logger) : Controller
{
    public async Task<IActionResult> Index()
    {
        var followUps = await dbContext.FollowUps
            .AsNoTracking()
            .Include(f => f.Inspection)
            .ThenInclude(i => i!.Premises)
            .OrderBy(f => f.Status)
            .ThenBy(f => f.DueDate)
            .ToListAsync();

        return View(followUps);
    }

    [Authorize(Roles = $"{ApplicationRoles.Admin},{ApplicationRoles.Inspector}")]
    public async Task<IActionResult> Create()
    {
        await PopulateInspectionsAsync();
        return View(new FollowUp { DueDate = DateTime.UtcNow.Date, Status = FollowUpStatus.Open });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = $"{ApplicationRoles.Admin},{ApplicationRoles.Inspector}")]
    public async Task<IActionResult> Create(FollowUp followUp)
    {
        if (!ModelState.IsValid)
        {
            await PopulateInspectionsAsync();
            return View(followUp);
        }

        var result = await followUpService.CreateAsync(followUp);
        if (!result.Success)
        {
            ModelState.AddModelError(string.Empty, result.Error ?? "Unable to create follow-up.");
            await PopulateInspectionsAsync();
            return View(followUp);
        }

        logger.LogInformation("Follow-up create request successful for inspection {InspectionId}", followUp.InspectionId);
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = $"{ApplicationRoles.Admin},{ApplicationRoles.Inspector}")]
    public IActionResult Close(int id)
    {
        return View(new FollowUpCloseViewModel { Id = id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = $"{ApplicationRoles.Admin},{ApplicationRoles.Inspector}")]
    public async Task<IActionResult> Close(FollowUpCloseViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var result = await followUpService.CloseAsync(model.Id, model.ClosedDate);
        if (!result.Success)
        {
            ModelState.AddModelError(string.Empty, result.Error ?? "Unable to close follow-up.");
            return View(model);
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task PopulateInspectionsAsync()
    {
        var inspections = await dbContext.Inspections
            .AsNoTracking()
            .Include(i => i.Premises)
            .OrderByDescending(i => i.InspectionDate)
            .Select(i => new { i.Id, Label = $"{i.Premises!.Name} ({i.InspectionDate:yyyy-MM-dd})" })
            .ToListAsync();

        ViewBag.InspectionId = new SelectList(inspections, "Id", "Label");
    }
}
