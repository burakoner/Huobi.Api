using Huobi.Api.Models.RestApi.Spot.Trading;

namespace Huobi.Api.Models.StreamApi.Spot;


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
    public IEnumerable<HuobiOrderBookEntry> Bids { get; set; } = Array.Empty<HuobiOrderBookEntry>();

    /// <summary>
    /// List of changed asks
    /// </summary>
    public IEnumerable<HuobiOrderBookEntry> Asks { get; set; } = Array.Empty<HuobiOrderBookEntry>();
}

