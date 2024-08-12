using System.Text.Json.Serialization;
using Credo.Common.Helpers;

namespace Credo.Domain.Entities;

public class OutboxMessage
{
    [JsonConverter(typeof(GuidConverter))]
    public Guid Id { get; set; }
    public required string Type { get; set; }
    public required string Data { get; set; }
    public DateTime OccurredOn { get; set; }
    public DateTime? ProcessedOn { get; set; }
}
