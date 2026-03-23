using System.ComponentModel.DataAnnotations;

namespace FoodSafetyInspectionTracker.Models.ViewModels;

public class FollowUpCloseViewModel
{
    public int Id { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime? ClosedDate { get; set; }
}
