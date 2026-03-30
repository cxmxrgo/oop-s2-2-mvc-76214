using System.ComponentModel.DataAnnotations;

namespace FoodSafetyInspectionTracker.Models.Validation;

public sealed class NotFutureDateAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is null)
        {
            return true;
        }

        if (value is DateTime date)
        {
            return date.Date <= DateTime.UtcNow.Date;
        }

        return false;
    }
}
