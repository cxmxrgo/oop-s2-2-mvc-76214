using FoodSafetyInspectionTracker.Data;
using FoodSafetyInspectionTracker.Models;
using FoodSafetyInspectionTracker.Models.ViewModels;
using FoodSafetyInspectionTracker.Services;
using Microsoft.EntityFrameworkCore;

namespace FoodSafetyInspectionTracker.Tests;

public class DashboardServiceTests
{
    [Fact]
    public async Task GetDashboardAsync_ReturnsExpectedCounts()
    {
        using var context = CreateContext();
        Seed(context);

        var service = new DashboardService(context);
        var result = await service.GetDashboardAsync(new DashboardFilter(), new DateTime(2026, 2, 15));

        Assert.Equal(2, result.InspectionsThisMonth);
        Assert.Equal(1, result.FailedInspectionsThisMonth);
        Assert.Equal(1, result.OpenFollowUpsOverdue);
    }

    [Fact]
    public async Task GetDashboardAsync_AppliesTownFilter()
    {
        using var context = CreateContext();
        Seed(context);

        var service = new DashboardService(context);
        var result = await service.GetDashboardAsync(new DashboardFilter { Town = "Riverton" }, new DateTime(2026, 2, 15));

        Assert.Equal(1, result.InspectionsThisMonth);
        Assert.Equal(0, result.FailedInspectionsThisMonth);
    }

    private static ApplicationDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbContext(options);
    }

    private static void Seed(ApplicationDbContext context)
    {
        context.Premises.AddRange(
            new Premises { Id = 1, Name = "One", Address = "A", Town = "Riverton", RiskRating = RiskRating.Low },
            new Premises { Id = 2, Name = "Two", Address = "B", Town = "Oakford", RiskRating = RiskRating.High });

        context.Inspections.AddRange(
            new Inspection { Id = 1, PremisesId = 1, InspectionDate = new DateTime(2026, 2, 5), Score = 80, Outcome = InspectionOutcome.Pass },
            new Inspection { Id = 2, PremisesId = 2, InspectionDate = new DateTime(2026, 2, 12), Score = 40, Outcome = InspectionOutcome.Fail },
            new Inspection { Id = 3, PremisesId = 1, InspectionDate = new DateTime(2026, 1, 12), Score = 90, Outcome = InspectionOutcome.Pass });

        context.FollowUps.AddRange(
            new FollowUp { Id = 1, InspectionId = 2, DueDate = new DateTime(2026, 2, 10), Status = FollowUpStatus.Open },
            new FollowUp { Id = 2, InspectionId = 3, DueDate = new DateTime(2026, 2, 10), Status = FollowUpStatus.Closed, ClosedDate = new DateTime(2026, 2, 10) });

        context.SaveChanges();
    }
}
