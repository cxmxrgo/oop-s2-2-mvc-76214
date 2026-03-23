using FoodSafetyInspectionTracker.Models;

namespace FoodSafetyInspectionTracker.Services;

public interface IFollowUpService
{
    Task<(bool Success, string? Error)> CreateAsync(FollowUp followUp);
    Task<(bool Success, string? Error)> CloseAsync(int id, DateTime? closedDate);
}
