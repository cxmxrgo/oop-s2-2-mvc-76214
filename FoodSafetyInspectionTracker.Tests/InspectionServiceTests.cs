using FoodSafetyInspectionTracker.Data;
using FoodSafetyInspectionTracker.Models;
using FoodSafetyInspectionTracker.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;

namespace FoodSafetyInspectionTracker.Tests;

public class InspectionServiceTests
{
    [Fact]
    public async Task CreateAsync_FailsWhenDuplicateOnSamePremisesAndDate()
    {
        using var context = CreateContext();
        context.Premises.Add(new Premises { Id = 1, Name = "Test", Address = "A", Town = "T", RiskRating = RiskRating.Low });
        context.Inspections.Add(new Inspection { Id = 1, PremisesId = 1, InspectionDate = new DateTime(2026, 2, 1), Score = 80, Outcome = InspectionOutcome.Pass });
        context.SaveChanges();

        var service = new InspectionService(context, NullLogger<InspectionService>.Instance);

        var result = await service.CreateAsync(new Inspection
        {
            PremisesId = 1,
            InspectionDate = new DateTime(2026, 2, 1, 15, 30, 0),
            Score = 75,
            Outcome = InspectionOutcome.Pass
        });

        Assert.False(result.Success);
        Assert.Equal("An inspection for this premises already exists on that date.", result.Error);
    }

    [Fact]
    public async Task CreateAsync_FailsWhenPremisesMissing()
    {
        using var context = CreateContext();
        var service = new InspectionService(context, NullLogger<InspectionService>.Instance);

        var result = await service.CreateAsync(new Inspection
        {
            PremisesId = 999,
            InspectionDate = new DateTime(2026, 2, 1),
            Score = 75,
            Outcome = InspectionOutcome.Pass
        });

        Assert.False(result.Success);
        Assert.Equal("Premises not found.", result.Error);
    }

    private static ApplicationDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbContext(options);
    }
}
