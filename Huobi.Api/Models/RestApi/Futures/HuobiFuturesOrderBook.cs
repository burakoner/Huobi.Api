namespace Huobi.Api.Models.RestApi.Futures;

/// <summary>
/// Order book
/// </summary>
public class HuobiFuturesOrderBook
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
    public IEnumerable<HuobiFuturesOrderBookEntry> Bids { get; set; } = Array.Empty<HuobiFuturesOrderBookEntry>();

    /// <summary>
    /// List of asks
    /// </summary>
    public IEnumerable<HuobiFuturesOrderBookEntry> Asks { get; set; } = Array.Empty<HuobiFuturesOrderBookEntry>();
}

