namespace Huobi.Api.Models.RestApi.Spot;

/// <summary>
/// Order book
/// </summary>
public class HuobiSpotOrderBook
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
    public IEnumerable<HuobiSpotOrderBookEntry> Bids { get; set; } = Array.Empty<HuobiSpotOrderBookEntry>();

    /// <summary>
    /// List of asks
    /// </summary>
    public IEnumerable<HuobiSpotOrderBookEntry> Asks { get; set; } = Array.Empty<HuobiSpotOrderBookEntry>();
}

