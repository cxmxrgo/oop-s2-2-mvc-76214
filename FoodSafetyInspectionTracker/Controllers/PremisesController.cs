using FoodSafetyInspectionTracker.Constants;
using FoodSafetyInspectionTracker.Data;
using FoodSafetyInspectionTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodSafetyInspectionTracker.Controllers;

[Authorize(Roles = $"{ApplicationRoles.Admin},{ApplicationRoles.Inspector},{ApplicationRoles.Viewer}")]
public class PremisesController(ApplicationDbContext dbContext, ILogger<PremisesController> logger) : Controller
{
    public async Task<IActionResult> Index()
    {
        return View(await dbContext.Premises.AsNoTracking().OrderBy(p => p.Town).ThenBy(p => p.Name).ToListAsync());
    }

    [Authorize(Roles = ApplicationRoles.Admin)]
    public IActionResult Create()
    {
        return View(new Premises());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = ApplicationRoles.Admin)]
    public async Task<IActionResult> Create(Premises premises)
    {
        if (!ModelState.IsValid)
        {
            logger.LogWarning("Premises create validation failed");
            return View(premises);
        }

        dbContext.Premises.Add(premises);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Premises created. PremisesId: {PremisesId}, Town: {Town}", premises.Id, premises.Town);
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = ApplicationRoles.Admin)]
    public async Task<IActionResult> Edit(int id)
    {
        var premises = await dbContext.Premises.FindAsync(id);
        return premises is null ? NotFound() : View(premises);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = ApplicationRoles.Admin)]
    public async Task<IActionResult> Edit(int id, Premises premises)
    {
        if (id != premises.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            logger.LogWarning("Premises update validation failed. PremisesId: {PremisesId}", premises.Id);
            return View(premises);
        }

        dbContext.Update(premises);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Premises updated. PremisesId: {PremisesId}, Town: {Town}", premises.Id, premises.Town);
        return RedirectToAction(nameof(Index));
    }
}
