using FoodSafetyInspectionTracker.Data;
using FoodSafetyInspectionTracker.Models;
using FoodSafetyInspectionTracker.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace FoodSafetyInspectionTracker.Services;

public class DashboardService(ApplicationDbContext dbContext) : IDashboardService
{
    public async Task<DashboardViewModel> GetDashboardAsync(DashboardFilter filter, DateTime today)
    {
        var monthStart = new DateTime(today.Year, today.Month, 1);
        var nextMonth = monthStart.AddMonths(1);

        var inspections = dbContext.Inspections
            .AsNoTracking()
            .Where(i => i.InspectionDate >= monthStart && i.InspectionDate < nextMonth)
            .Include(i => i.Premises)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Town))
        {
            inspections = inspections.Where(i => i.Premises!.Town == filter.Town);
        }

        if (filter.RiskRating.HasValue)
        {
            inspections = inspections.Where(i => i.Premises!.RiskRating == filter.RiskRating.Value);
        }

        var overdueFollowUps = dbContext.FollowUps
            .AsNoTracking()
            .Include(f => f.Inspection)
            .ThenInclude(i => i!.Premises)
            .Where(f => f.Status == FollowUpStatus.Open && f.DueDate.Date < today.Date)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Town))
        {
            overdueFollowUps = overdueFollowUps.Where(f => f.Inspection!.Premises!.Town == filter.Town);
        }

        if (filter.RiskRating.HasValue)
        {
            overdueFollowUps = overdueFollowUps.Where(f => f.Inspection!.Premises!.RiskRating == filter.RiskRating.Value);
        }

        return new DashboardViewModel
        {
            Town = filter.Town,
            RiskRating = filter.RiskRating,
            InspectionsThisMonth = await inspections.CountAsync(),
            FailedInspectionsThisMonth = await inspections.CountAsync(i => i.Outcome == InspectionOutcome.Fail),
            OpenFollowUpsOverdue = await overdueFollowUps.CountAsync(),
            AvailableTowns = await dbContext.Premises.AsNoTracking().Select(p => p.Town).Distinct().OrderBy(t => t).ToListAsync()
        };
    }
}
