using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BackendPlatform.API.UserProfileManagement.Infrastructure.Persistence.EntityFrameworkCore.Configuration.ValueConverters;

public sealed class DateOnlyDateTimeValueConverter() : ValueConverter<DateOnly, DateTime>(
    date => date.ToDateTime(TimeOnly.MinValue),
    
    dateTime => DateOnly.FromDateTime(dateTime)
    );
