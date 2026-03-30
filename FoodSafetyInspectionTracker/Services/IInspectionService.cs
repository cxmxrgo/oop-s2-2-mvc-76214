using FoodSafetyInspectionTracker.Models;

namespace FoodSafetyInspectionTracker.Services;

public interface IInspectionService
{
    Task<(bool Success, string? Error)> CreateAsync(Inspection inspection);
}
