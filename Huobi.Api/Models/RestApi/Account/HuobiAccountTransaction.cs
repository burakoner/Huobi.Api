namespace Huobi.Api.Models.RestApi.Account;

/// <summary>
/// Transaction result
/// </summary>
public class HuobiAccountTransaction
{
    /// <summary>
    /// Id
    /// </summary>
    [JsonProperty("transact-id")]
    public long TransactionId { get; set; }

    /// <summary>
    /// Time
    /// </summary>
    [JsonProperty("transact-time"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime TransactionTime { get; set; }
}
