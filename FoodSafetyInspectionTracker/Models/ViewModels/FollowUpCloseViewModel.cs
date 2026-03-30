using System.ComponentModel.DataAnnotations;
using FoodSafetyInspectionTracker.Models.Validation;

namespace FoodSafetyInspectionTracker.Models.ViewModels;

public class FollowUpCloseViewModel
{
    public int Id { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [NotFutureDate(ErrorMessage = "Closed date cannot be in the future.")]
    public DateTime? ClosedDate { get; set; }
}
