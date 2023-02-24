namespace Huobi.Api.Models.RestApi.Spot;


/// <summary>
/// Incremental order book update
/// </summary>
public class HuobiIncementalOrderBook
{
    /// <summary>
    /// Sequence number
    /// </summary>
    [JsonProperty("seqNum")]
    public long SequenceNumber { get; set; }

    /// <summary>
    /// Previous sequence number
    /// </summary>
    [JsonProperty("prevSeqNum")]
    public long? PreviousSequenceNumber { get; set; }

    /// <summary>
    /// List of changed bids
    /// </summary>
    public IEnumerable<HuobiSpotOrderBookEntry> Bids { get; set; } = Array.Empty<HuobiSpotOrderBookEntry>();

    /// <summary>
    /// List of changed asks
    /// </summary>
    public IEnumerable<HuobiSpotOrderBookEntry> Asks { get; set; } = Array.Empty<HuobiSpotOrderBookEntry>();
}

