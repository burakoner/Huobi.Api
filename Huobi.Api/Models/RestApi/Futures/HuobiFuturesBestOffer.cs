namespace Huobi.Api.Models.RestApi.Futures;

/// <summary>
/// Order book
/// </summary>
public class HuobiFuturesBestOffer
{
    public string Symbol { get; set; }
    
    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// Version
    /// </summary>
        [JsonProperty("mrid")]
    public long Id { get; set; }

    /// <summary>
    /// List of bids
    /// </summary>
    public HuobiFuturesOrderBookEntry Bid { get; set; }

    /// <summary>
    /// List of asks
    /// </summary>
    public HuobiFuturesOrderBookEntry Ask { get; set; }
}

