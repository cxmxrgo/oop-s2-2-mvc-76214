using FoodSafetyInspectionTracker.Data;
using FoodSafetyInspectionTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodSafetyInspectionTracker.Services;

public class InspectionService(ApplicationDbContext dbContext, ILogger<InspectionService> logger) : IInspectionService
{
    public async Task<(bool Success, string? Error)> CreateAsync(Inspection inspection)
    {
        var premisesExists = await dbContext.Premises.AsNoTracking().AnyAsync(p => p.Id == inspection.PremisesId);
        if (!premisesExists)
        {
            logger.LogWarning("Inspection creation failed because premises {PremisesId} was not found", inspection.PremisesId);
            return (false, "Premises not found.");
        }

        var date = inspection.InspectionDate.Date;
        var hasDuplicate = await dbContext.Inspections.AsNoTracking().AnyAsync(i =>
            i.PremisesId == inspection.PremisesId &&
            i.InspectionDate >= date &&
            i.InspectionDate < date.AddDays(1));

        if (hasDuplicate)
        {
            logger.LogWarning("Inspection creation blocked due to duplicate inspection on same date. PremisesId: {PremisesId}, InspectionDate: {InspectionDate}",
                inspection.PremisesId, inspection.InspectionDate);
            return (false, "An inspection for this premises already exists on that date.");
        }

        dbContext.Inspections.Add(inspection);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Inspection created. PremisesId: {PremisesId}, InspectionId: {InspectionId}, Outcome: {Outcome}",
            inspection.PremisesId, inspection.Id, inspection.Outcome);

        return (true, null);
    }
}
