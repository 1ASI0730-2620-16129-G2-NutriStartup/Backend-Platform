using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Acme.Center.Platform.AppointmentManagement.Infrastructure.Persistence.EntityFrameworkCore.Configuration.ValueConverters;

/// <summary>
///     Value converter that stores a <see cref="TimeOnly" /> as a database time.
/// </summary>
public class TimeOnlyTimeSpanValueConverter() : ValueConverter<TimeOnly, TimeSpan>(
    time => time.ToTimeSpan(),
    timeSpan => TimeOnly.FromTimeSpan(timeSpan));
