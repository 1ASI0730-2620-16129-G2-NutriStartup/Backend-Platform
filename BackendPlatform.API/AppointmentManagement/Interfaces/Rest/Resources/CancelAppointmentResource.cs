using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Acme.Center.Platform.AppointmentManagement.Interfaces.Rest.Converters;

namespace Acme.Center.Platform.AppointmentManagement.Interfaces.Rest.Resources;

/// <summary>
///     Cancel appointment resource for REST API
/// </summary>
/// <param name="Reason">The optional reason of the cancellation</param>
public record CancelAppointmentResource([property: MaxLength(500)] string? Reason);
