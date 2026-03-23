using FoodSafetyInspectionTracker.Data;
using FoodSafetyInspectionTracker.Models;
using FoodSafetyInspectionTracker.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;

namespace FoodSafetyInspectionTracker.Tests;

public class FollowUpServiceTests
{
    [Fact]
    public async Task CloseAsync_FailsWhenClosedDateMissing()
    {
        using var context = CreateContext();
        context.Premises.Add(new Premises { Id = 1, Name = "Test", Address = "A", Town = "T", RiskRating = RiskRating.Low });
        context.Inspections.Add(new Inspection { Id = 1, PremisesId = 1, InspectionDate = new DateTime(2026, 2, 1), Score = 55, Outcome = InspectionOutcome.Fail });
        context.FollowUps.Add(new FollowUp { Id = 1, InspectionId = 1, DueDate = new DateTime(2026, 2, 10), Status = FollowUpStatus.Open });
        context.SaveChanges();

        var service = new FollowUpService(context, NullLogger<FollowUpService>.Instance);

        var result = await service.CloseAsync(1, null);

        Assert.False(result.Success);
        Assert.Equal("Closed date is required.", result.Error);
    }

    [Fact]
    public async Task CreateAsync_FailsWhenDueDateBeforeInspection()
    {
        using var context = CreateContext();
        context.Premises.Add(new Premises { Id = 1, Name = "Test", Address = "A", Town = "T", RiskRating = RiskRating.Low });
        context.Inspections.Add(new Inspection { Id = 1, PremisesId = 1, InspectionDate = new DateTime(2026, 2, 1), Score = 55, Outcome = InspectionOutcome.Fail });
        context.SaveChanges();

        var service = new FollowUpService(context, NullLogger<FollowUpService>.Instance);

        var result = await service.CreateAsync(new FollowUp { InspectionId = 1, DueDate = new DateTime(2026, 1, 31) });

        Assert.False(result.Success);
        Assert.Equal("Due date cannot be before inspection date.", result.Error);
    }

    private static ApplicationDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbContext(options);
    }
}
