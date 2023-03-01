namespace Huobi.Api.Models.RestApi.Spot.Public;

/// <summary>
/// Status of the symbol
/// </summary>
public class HuobiSystemStatus
{
    /// <summary>
    /// The status
    /// </summary>
    [JsonProperty("marketStatus"), JsonConverter(typeof(MarketStatusConverter))]
    public MarketStatus Status { get; set; }

    /// <summary>
    /// Start time of when market halted
    /// </summary>
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? HaltStartTime { get; set; }

    /// <summary>
    /// Estimated end time of the halt
    /// </summary>
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? HaltEndTime { get; set; }

    /// <summary>
    /// Reason for halting
    /// </summary>
    public string HaltReason { get; set; }

    /// <summary>
    /// Affected symbols, comma separated or 'all' if all symbols are affected
    /// </summary>
    public string AffectedSymbols { get; set; }
}
