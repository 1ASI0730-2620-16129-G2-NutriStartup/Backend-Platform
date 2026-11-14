using Acme.Center.Platform.AppointmentManagement.Domain.Model.Aggregates;
using Acme.Center.Platform.AppointmentManagement.Domain.Model.ValueObjects;
using Acme.Center.Platform.AppointmentManagement.Infrastructure.Persistence.EntityFrameworkCore.Configuration.ValueConverters;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.ValueConverters;
using Microsoft.EntityFrameworkCore;

namespace Acme.Center.Platform.AppointmentManagement.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyAppointmentManagementConfiguration(this ModelBuilder builder)
    {
        // Availability
        builder.Entity<Availability>().HasKey(a => a.Id);
        builder.Entity<Availability>().Property(a => a.Id)
            .HasConversion(new StronglyTypedIdConverter<AvailabilityId, int>(id => id.Value,
                value => new AvailabilityId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<Availability>().Property(a => a.NutritionistId)
            .HasConversion(new StronglyTypedIdConverter<NutritionistId, int>(id => id.Value,
                value => new NutritionistId(value)))
            .IsRequired();
        builder.Entity<Availability>().Property(a => a.Date)
            .HasConversion(new DateOnlyDateTimeValueConverter())
            .IsRequired();
        builder.Entity<Availability>().Property(a => a.StartTime)
            .HasConversion(new TimeOnlyTimeSpanValueConverter())
            .IsRequired();
        builder.Entity<Availability>().Property(a => a.EndTime)
            .HasConversion(new TimeOnlyTimeSpanValueConverter())
            .IsRequired();
        // Concurrency token: two patients can never reserve the same slot, the second UPDATE fails.
        builder.Entity<Availability>().Property(a => a.IsAvailable).IsRequired().IsConcurrencyToken();
        builder.Entity<Availability>().Ignore(a => a.Slot);
        builder.Entity<Availability>().HasIndex(a => new { a.NutritionistId, a.Date });

        // Appointment
        builder.Entity<Appointment>().HasKey(a => a.Id);
        builder.Entity<Appointment>().Property(a => a.Id)
            .HasConversion(new StronglyTypedIdConverter<AppointmentId, int>(id => id.Value,
                value => new AppointmentId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<Appointment>().Property(a => a.PatientId)
            .HasConversion(new StronglyTypedIdConverter<PatientId, int>(id => id.Value,
                value => new PatientId(value)))
            .IsRequired();
        builder.Entity<Appointment>().Property(a => a.NutritionistId)
            .HasConversion(new StronglyTypedIdConverter<NutritionistId, int>(id => id.Value,
                value => new NutritionistId(value)))
            .IsRequired();
        // The availability is referenced by identity only (no foreign key): aggregates are consistency boundaries.
        builder.Entity<Appointment>().Property(a => a.AvailabilityId)
            .HasConversion(new StronglyTypedIdConverter<AvailabilityId, int>(id => id.Value,
                value => new AvailabilityId(value)))
            .IsRequired();
        builder.Entity<Appointment>().Property(a => a.Date)
            .HasConversion(new DateOnlyDateTimeValueConverter())
            .IsRequired();
        builder.Entity<Appointment>().Property(a => a.StartTime)
            .HasConversion(new TimeOnlyTimeSpanValueConverter())
            .IsRequired();
        builder.Entity<Appointment>().Property(a => a.EndTime)
            .HasConversion(new TimeOnlyTimeSpanValueConverter())
            .IsRequired();
        builder.Entity<Appointment>().Property(a => a.Reason).IsRequired().HasMaxLength(500);
        builder.Entity<Appointment>().Property(a => a.Status)
            .HasConversion<string>()
            .IsRequired()
            .HasMaxLength(20);
        builder.Entity<Appointment>().Property(a => a.DataAccessAuthorized).IsRequired();
        builder.Entity<Appointment>().Property(a => a.RescheduleCount).IsRequired();
        builder.Entity<Appointment>().Property(a => a.CancellationReason).HasMaxLength(500);
        builder.Entity<Appointment>().Ignore(a => a.Slot);
        builder.Entity<Appointment>().HasIndex(a => a.PatientId);
        builder.Entity<Appointment>().HasIndex(a => a.NutritionistId);

        // Consultation
        builder.Entity<Consultation>().HasKey(c => c.Id);
        builder.Entity<Consultation>().Property(c => c.Id)
            .HasConversion(new StronglyTypedIdConverter<ConsultationId, int>(id => id.Value,
                value => new ConsultationId(value)))
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Entity<Consultation>().Property(c => c.AppointmentId)
            .HasConversion(new StronglyTypedIdConverter<AppointmentId, int>(id => id.Value,
                value => new AppointmentId(value)))
            .IsRequired();
        builder.Entity<Consultation>().Property(c => c.PatientId)
            .HasConversion(new StronglyTypedIdConverter<PatientId, int>(id => id.Value,
                value => new PatientId(value)))
            .IsRequired();
        builder.Entity<Consultation>().Property(c => c.NutritionistId)
            .HasConversion(new StronglyTypedIdConverter<NutritionistId, int>(id => id.Value,
                value => new NutritionistId(value)))
            .IsRequired();
        builder.Entity<Consultation>().Property(c => c.Notes).IsRequired().HasMaxLength(2000);
        builder.Entity<Consultation>().Property(c => c.Recommendations).IsRequired().HasMaxLength(2000);
        builder.Entity<Consultation>().Property(c => c.Date)
            .HasConversion(new DateOnlyDateTimeValueConverter())
            .IsRequired();
        builder.Entity<Consultation>().Property(c => c.FollowUpDate)
            .HasConversion(new DateOnlyDateTimeValueConverter());
        // One consultation per appointment.
        builder.Entity<Consultation>().HasIndex(c => c.AppointmentId).IsUnique();
    }
}
