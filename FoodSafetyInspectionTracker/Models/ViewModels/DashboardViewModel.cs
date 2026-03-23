namespace FoodSafetyInspectionTracker.Models.ViewModels;

public class DashboardViewModel
{
    public int InspectionsThisMonth { get; set; }
    public int FailedInspectionsThisMonth { get; set; }
    public int OpenFollowUpsOverdue { get; set; }
    public string? Town { get; set; }
    public RiskRating? RiskRating { get; set; }
    public List<string> AvailableTowns { get; set; } = [];
}
