using FoodSafetyInspectionTracker.Data;
using FoodSafetyInspectionTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodSafetyInspectionTracker.Services;

public class FollowUpService(ApplicationDbContext dbContext, ILogger<FollowUpService> logger) : IFollowUpService
{
    public async Task<(bool Success, string? Error)> CreateAsync(FollowUp followUp)
    {
        var inspection = await dbContext.Inspections.AsNoTracking().FirstOrDefaultAsync(i => i.Id == followUp.InspectionId);
        if (inspection is null)
        {
            logger.LogWarning("Follow-up creation failed because inspection {InspectionId} was not found", followUp.InspectionId);
            return (false, "Inspection not found.");
        }

        if (followUp.DueDate.Date < inspection.InspectionDate.Date)
        {
            logger.LogWarning("Follow-up creation blocked due to due date before inspection date. InspectionId: {InspectionId}, DueDate: {DueDate}, InspectionDate: {InspectionDate}",
                followUp.InspectionId, followUp.DueDate, inspection.InspectionDate);
            return (false, "Due date cannot be before inspection date.");
        }

        dbContext.FollowUps.Add(followUp);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Follow-up created. FollowUpId: {FollowUpId}, InspectionId: {InspectionId}", followUp.Id, followUp.InspectionId);
        return (true, null);
    }

    public async Task<(bool Success, string? Error)> CloseAsync(int id, DateTime? closedDate)
    {
        var followUp = await dbContext.FollowUps.FirstOrDefaultAsync(f => f.Id == id);
        if (followUp is null)
        {
            logger.LogWarning("Close follow-up failed because follow-up {FollowUpId} was not found", id);
            return (false, "Follow-up not found.");
        }

        if (!closedDate.HasValue)
        {
            logger.LogWarning("Close follow-up blocked because closed date was missing. FollowUpId: {FollowUpId}", id);
            return (false, "Closed date is required.");
        }

        followUp.Status = FollowUpStatus.Closed;
        followUp.ClosedDate = closedDate.Value.Date;

        await dbContext.SaveChangesAsync();
        logger.LogInformation("Follow-up closed. FollowUpId: {FollowUpId}, ClosedDate: {ClosedDate}", followUp.Id, followUp.ClosedDate);
        return (true, null);
    }
}
