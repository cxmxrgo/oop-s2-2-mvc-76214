using FoodSafetyInspectionTracker.Controllers;
using Microsoft.AspNetCore.Authorization;

namespace FoodSafetyInspectionTracker.Tests;

public class AuthorizationTests
{
    [Fact]
    public void FollowUpsController_RequiresInspectorOrAdminForCreateAction()
    {
        var method = typeof(FollowUpsController).GetMethod(nameof(FollowUpsController.Create), [typeof(FoodSafetyInspectionTracker.Models.FollowUp)]);
        var attribute = method?.GetCustomAttributes(typeof(AuthorizeAttribute), true).Cast<AuthorizeAttribute>().FirstOrDefault();

        Assert.NotNull(attribute);
        Assert.Equal("Admin,Inspector", attribute!.Roles);
    }
}
