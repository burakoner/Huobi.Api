namespace Huobi.Api.Models.RestApi.Spot.Trading;

/// <summary>
/// Order book
/// </summary>
public class HuobiOrderBook
{
    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Version
    /// </summary>
    public long Version { get; set; }

    /// <summary>
    /// List of bids
    /// </summary>
    public IEnumerable<HuobiOrderBookEntry> Bids { get; set; } = Array.Empty<HuobiOrderBookEntry>();

    /// <summary>
    /// List of asks
    /// </summary>
    public IEnumerable<HuobiOrderBookEntry> Asks { get; set; } = Array.Empty<HuobiOrderBookEntry>();
}

