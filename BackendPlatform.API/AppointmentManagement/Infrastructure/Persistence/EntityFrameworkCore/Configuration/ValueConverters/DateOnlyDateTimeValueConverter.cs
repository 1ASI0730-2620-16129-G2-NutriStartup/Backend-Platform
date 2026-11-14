using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Acme.Center.Platform.AppointmentManagement.Infrastructure.Persistence.EntityFrameworkCore.Configuration.ValueConverters;

/// <summary>
///     Value converter that stores a <see cref="DateOnly" /> as a database date/time (at midnight).
/// </summary>
public class DateOnlyDateTimeValueConverter() : ValueConverter<DateOnly, DateTime>(
    date => date.ToDateTime(TimeOnly.MinValue),
    dateTime => DateOnly.FromDateTime(dateTime));
