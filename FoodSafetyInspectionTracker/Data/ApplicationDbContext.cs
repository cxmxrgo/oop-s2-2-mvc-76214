using FoodSafetyInspectionTracker.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FoodSafetyInspectionTracker.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {
        public DbSet<Premises> Premises => Set<Premises>();
        public DbSet<Inspection> Inspections => Set<Inspection>();
        public DbSet<FollowUp> FollowUps => Set<FollowUp>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Premises>()
                .HasMany(p => p.Inspections)
                .WithOne(i => i.Premises)
                .HasForeignKey(i => i.PremisesId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Inspection>()
                .HasMany(i => i.FollowUps)
                .WithOne(f => f.Inspection)
                .HasForeignKey(f => f.InspectionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Premises>().HasData(
                new Premises { Id = 1, Name = "Harbour Deli", Address = "10 Dock Lane", Town = "Riverton", RiskRating = RiskRating.Low },
                new Premises { Id = 2, Name = "Town Bakery", Address = "22 Market St", Town = "Riverton", RiskRating = RiskRating.Medium },
                new Premises { Id = 3, Name = "Sunrise Cafe", Address = "8 Green Rd", Town = "Riverton", RiskRating = RiskRating.High },
                new Premises { Id = 4, Name = "North Grill", Address = "14 Hill Ave", Town = "Oakford", RiskRating = RiskRating.Medium },
                new Premises { Id = 5, Name = "Oakford Sushi", Address = "2 River Walk", Town = "Oakford", RiskRating = RiskRating.High },
                new Premises { Id = 6, Name = "Family Pizza", Address = "40 Cedar Dr", Town = "Oakford", RiskRating = RiskRating.Low },
                new Premises { Id = 7, Name = "Meadow Bistro", Address = "7 Lake View", Town = "Kingswell", RiskRating = RiskRating.Medium },
                new Premises { Id = 8, Name = "Kingswell Tacos", Address = "18 Mill Road", Town = "Kingswell", RiskRating = RiskRating.High },
                new Premises { Id = 9, Name = "Noodle House", Address = "31 Station Rd", Town = "Kingswell", RiskRating = RiskRating.Low },
                new Premises { Id = 10, Name = "Garden Eatery", Address = "9 Orchard Way", Town = "Riverton", RiskRating = RiskRating.Medium },
                new Premises { Id = 11, Name = "Riverside Pub", Address = "5 Quay St", Town = "Oakford", RiskRating = RiskRating.High },
                new Premises { Id = 12, Name = "Campus Canteen", Address = "1 College Green", Town = "Kingswell", RiskRating = RiskRating.Medium }
            );

            builder.Entity<Inspection>().HasData(
                new Inspection { Id = 1, PremisesId = 1, InspectionDate = new DateTime(2026, 1, 10), Score = 92, Outcome = InspectionOutcome.Pass, Notes = "Good hygiene." },
                new Inspection { Id = 2, PremisesId = 2, InspectionDate = new DateTime(2026, 1, 11), Score = 71, Outcome = InspectionOutcome.Pass, Notes = "Minor improvements needed." },
                new Inspection { Id = 3, PremisesId = 3, InspectionDate = new DateTime(2026, 1, 12), Score = 49, Outcome = InspectionOutcome.Fail, Notes = "Storage issue." },
                new Inspection { Id = 4, PremisesId = 4, InspectionDate = new DateTime(2026, 1, 14), Score = 80, Outcome = InspectionOutcome.Pass, Notes = "Compliant." },
                new Inspection { Id = 5, PremisesId = 5, InspectionDate = new DateTime(2026, 1, 15), Score = 55, Outcome = InspectionOutcome.Fail, Notes = "Cross contamination controls weak." },
                new Inspection { Id = 6, PremisesId = 6, InspectionDate = new DateTime(2026, 1, 17), Score = 88, Outcome = InspectionOutcome.Pass, Notes = "Solid process." },
                new Inspection { Id = 7, PremisesId = 7, InspectionDate = new DateTime(2026, 1, 20), Score = 64, Outcome = InspectionOutcome.Fail, Notes = "Temperature log missing." },
                new Inspection { Id = 8, PremisesId = 8, InspectionDate = new DateTime(2026, 1, 22), Score = 76, Outcome = InspectionOutcome.Pass, Notes = "Adequate." },
                new Inspection { Id = 9, PremisesId = 9, InspectionDate = new DateTime(2026, 1, 24), Score = 91, Outcome = InspectionOutcome.Pass, Notes = "Very clean." },
                new Inspection { Id = 10, PremisesId = 10, InspectionDate = new DateTime(2026, 1, 25), Score = 70, Outcome = InspectionOutcome.Pass, Notes = "Training reminder." },
                new Inspection { Id = 11, PremisesId = 11, InspectionDate = new DateTime(2026, 1, 26), Score = 45, Outcome = InspectionOutcome.Fail, Notes = "Pest control action required." },
                new Inspection { Id = 12, PremisesId = 12, InspectionDate = new DateTime(2026, 1, 27), Score = 86, Outcome = InspectionOutcome.Pass, Notes = "Good standard." },
                new Inspection { Id = 13, PremisesId = 1, InspectionDate = new DateTime(2026, 2, 3), Score = 90, Outcome = InspectionOutcome.Pass, Notes = "Routine check." },
                new Inspection { Id = 14, PremisesId = 2, InspectionDate = new DateTime(2026, 2, 5), Score = 52, Outcome = InspectionOutcome.Fail, Notes = "Cleanliness issue." },
                new Inspection { Id = 15, PremisesId = 3, InspectionDate = new DateTime(2026, 2, 7), Score = 61, Outcome = InspectionOutcome.Fail, Notes = "Record keeping poor." },
                new Inspection { Id = 16, PremisesId = 4, InspectionDate = new DateTime(2026, 2, 8), Score = 82, Outcome = InspectionOutcome.Pass, Notes = "Improved." },
                new Inspection { Id = 17, PremisesId = 5, InspectionDate = new DateTime(2026, 2, 10), Score = 59, Outcome = InspectionOutcome.Fail, Notes = "Equipment sanitization required." },
                new Inspection { Id = 18, PremisesId = 6, InspectionDate = new DateTime(2026, 2, 11), Score = 84, Outcome = InspectionOutcome.Pass, Notes = "Satisfactory." },
                new Inspection { Id = 19, PremisesId = 7, InspectionDate = new DateTime(2026, 2, 13), Score = 79, Outcome = InspectionOutcome.Pass, Notes = "Minor notes." },
                new Inspection { Id = 20, PremisesId = 8, InspectionDate = new DateTime(2026, 2, 14), Score = 47, Outcome = InspectionOutcome.Fail, Notes = "Cleaning schedule not followed." },
                new Inspection { Id = 21, PremisesId = 9, InspectionDate = new DateTime(2026, 2, 16), Score = 94, Outcome = InspectionOutcome.Pass, Notes = "Excellent." },
                new Inspection { Id = 22, PremisesId = 10, InspectionDate = new DateTime(2026, 2, 18), Score = 66, Outcome = InspectionOutcome.Fail, Notes = "Food storage issue." },
                new Inspection { Id = 23, PremisesId = 11, InspectionDate = new DateTime(2026, 2, 19), Score = 74, Outcome = InspectionOutcome.Pass, Notes = "Corrective actions in place." },
                new Inspection { Id = 24, PremisesId = 12, InspectionDate = new DateTime(2026, 2, 21), Score = 83, Outcome = InspectionOutcome.Pass, Notes = "Good condition." },
                new Inspection { Id = 25, PremisesId = 5, InspectionDate = new DateTime(2026, 2, 24), Score = 69, Outcome = InspectionOutcome.Fail, Notes = "Staff hygiene retraining." }
            );

            builder.Entity<FollowUp>().HasData(
                new FollowUp { Id = 1, InspectionId = 3, DueDate = new DateTime(2026, 1, 20), Status = FollowUpStatus.Open, ClosedDate = null },
                new FollowUp { Id = 2, InspectionId = 5, DueDate = new DateTime(2026, 1, 28), Status = FollowUpStatus.Closed, ClosedDate = new DateTime(2026, 1, 27) },
                new FollowUp { Id = 3, InspectionId = 7, DueDate = new DateTime(2026, 2, 1), Status = FollowUpStatus.Open, ClosedDate = null },
                new FollowUp { Id = 4, InspectionId = 11, DueDate = new DateTime(2026, 2, 4), Status = FollowUpStatus.Closed, ClosedDate = new DateTime(2026, 2, 2) },
                new FollowUp { Id = 5, InspectionId = 14, DueDate = new DateTime(2026, 2, 10), Status = FollowUpStatus.Open, ClosedDate = null },
                new FollowUp { Id = 6, InspectionId = 15, DueDate = new DateTime(2026, 2, 14), Status = FollowUpStatus.Open, ClosedDate = null },
                new FollowUp { Id = 7, InspectionId = 17, DueDate = new DateTime(2026, 2, 18), Status = FollowUpStatus.Closed, ClosedDate = new DateTime(2026, 2, 17) },
                new FollowUp { Id = 8, InspectionId = 20, DueDate = new DateTime(2026, 2, 20), Status = FollowUpStatus.Open, ClosedDate = null },
                new FollowUp { Id = 9, InspectionId = 22, DueDate = new DateTime(2026, 2, 24), Status = FollowUpStatus.Open, ClosedDate = null },
                new FollowUp { Id = 10, InspectionId = 25, DueDate = new DateTime(2026, 2, 27), Status = FollowUpStatus.Open, ClosedDate = null }
            );
        }
    }
}
