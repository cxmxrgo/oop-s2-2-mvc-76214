using System.ComponentModel.DataAnnotations;

namespace FoodSafetyInspectionTracker.Models;

public class Premises
{
    public int Id { get; set; }

    [Required]
    [StringLength(120)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    public string Address { get; set; } = string.Empty;

    [Required]
    [StringLength(80)]
    public string Town { get; set; } = string.Empty;

    [Required]
    [EnumDataType(typeof(RiskRating))]
    public RiskRating RiskRating { get; set; }

    public ICollection<Inspection> Inspections { get; set; } = [];
}
