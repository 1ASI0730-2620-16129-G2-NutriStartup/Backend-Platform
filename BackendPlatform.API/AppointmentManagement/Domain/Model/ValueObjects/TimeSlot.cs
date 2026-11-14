using System.Globalization;

namespace Acme.Center.Platform.AppointmentManagement.Domain.Model.ValueObjects;

/// <summary>
///     Value object that represents a time slot (horario): a day plus a start and an end time.
/// </summary>
/// <param name="Date">The day of the slot.</param>
/// <param name="StartTime">The time at which the slot starts.</param>
/// <param name="EndTime">The time at which the slot ends. Must be later than <paramref name="StartTime" />.</param>
public record TimeSlot(DateOnly Date, TimeOnly StartTime, TimeOnly EndTime)
{
    /// <summary>
    ///     Parameterless constructor required by persistence mechanisms.
    /// </summary>
    public TimeSlot() : this(default, default, default)
    {
    }

    /// <summary>
    ///     Gets a value indicating whether the slot is well formed (it ends after it starts).
    /// </summary>
    public bool IsValid => EndTime > StartTime;

    /// <summary>
    ///     Determines whether this slot shares any time with another slot of the same day.
    /// </summary>
    /// <param name="other">The slot to compare with.</param>
    /// <returns>True when both slots overlap, false otherwise.</returns>
    public bool OverlapsWith(TimeSlot other)
    {
        return Date == other.Date && StartTime < other.EndTime && EndTime > other.StartTime;
    }

    /// <summary>
    ///     Gets a culture-invariant human readable representation, for example <c>2026-10-12 10:00-11:00</c>.
    /// </summary>
    public string Describe()
    {
        var culture = CultureInfo.InvariantCulture;
        return $"{Date.ToString("yyyy-MM-dd", culture)} " +
               $"{StartTime.ToString("HH:mm", culture)}-{EndTime.ToString("HH:mm", culture)}";
    }
}
