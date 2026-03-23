using FoodSafetyInspectionTracker.Models.ViewModels;

namespace FoodSafetyInspectionTracker.Services;

public interface IDashboardService
{
    Task<DashboardViewModel> GetDashboardAsync(DashboardFilter filter, DateTime today);
}
