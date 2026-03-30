using System.ComponentModel.DataAnnotations;

namespace FoodSafetyInspectionTracker.Models;

public class FollowUp
{
    public int Id { get; set; }

    [Required]
    public int InspectionId { get; set; }

    [Required]
    public DateTime DueDate { get; set; }

    [Required]
    [EnumDataType(typeof(FollowUpStatus))]
    public FollowUpStatus Status { get; set; } = FollowUpStatus.Open;

    public DateTime? ClosedDate { get; set; }

    public Inspection? Inspection { get; set; }
}
