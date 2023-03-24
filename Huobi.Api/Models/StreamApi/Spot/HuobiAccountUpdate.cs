namespace Huobi.Api.Models.StreamApi.Spot;

/// <summary>
/// Account update
/// </summary>
public class HuobiAccountUpdate
{
    /// <summary>
    /// Asset
    /// </summary>
    [JsonProperty("currency")]
    public string Asset { get; set; } = string.Empty;
    /// <summary>
    /// Account id
    /// </summary>
    public long AccountId { get; set; }
    /// <summary>
    /// Total balance
    /// </summary>
    public decimal? Balance { get; set; }
    /// <summary>
    /// Available balance
    /// </summary>
    public decimal? Available { get; set; }
    /// <summary>
    /// Type of change
    /// </summary>
    [JsonConverter(typeof(AccountEventConverter))]
    public AccountEventType? ChangeType { get; set; }
    /// <summary>
    /// Account type
    /// </summary>
    [JsonConverter(typeof(BalanceTypeConverter))]
    public BalanceType AccountType { get; set; }
    /// <summary>
    /// Change time
    /// </summary>
    [JsonConverter(typeof(DateTimeConverter))]
    public DateTime? ChangeTime { get; set; }
    /// <summary>
    /// Update sequence number
    /// </summary>
    [JsonProperty("seqNum")]
    public long SequenceNumber { get; set; }
}
