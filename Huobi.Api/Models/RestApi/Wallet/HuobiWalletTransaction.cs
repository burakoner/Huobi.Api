namespace Huobi.Api.Models.RestApi.Wallet;

/// <summary>
/// Withdraw or Deposit
/// </summary>
public class HuobiWalletTransaction
{
    /// <summary>
    /// Transfer id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Define transfer type to search, possible values: [deposit, withdraw]
    /// </summary>
    [JsonProperty("type"), JsonConverter(typeof(WalletTransactionTypeConverter))]
    public WalletTransactionType Type { get; set; }

    /// <summary>
    /// Sub type
    /// </summary>
    [JsonProperty("sub-type")]
    public string SubType { get; set; } = string.Empty;

    /// <summary>
    /// The crypto asset to withdraw
    /// </summary>
    [JsonProperty("currency")]
    public string Asset { get; set; }

    /// <summary>
    /// Block chain name
    /// </summary>
    [JsonProperty("chain")]
    public string Network { get; set; }

    /// <summary>
    /// The on-chain transaction hash
    /// </summary>
    [JsonProperty("tx-hash")]
    public string TransactionHash { get; set; }

    /// <summary>
    /// The number of crypto asset transfered in its minimum unit
    /// </summary>
    [JsonProperty("amount")]
    public decimal Quantity { get; set; }

    /// <summary>
    /// The address tag of the address its from
    /// </summary>
    [JsonProperty("from-addr-tag")]
    public string FromAddressTag { get; set; }

    /// <summary>
    /// The deposit or withdraw target address
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// The user defined address tag
    /// </summary>
    [JsonProperty("address-tag")]
    public string AddressTag { get; set; }

    /// <summary>
    /// Withdraw fee
    /// </summary>
    public decimal Fee { get; set; }

    /// <summary>
    /// The state of this transfer
    /// </summary>
    [JsonProperty("state"), JsonConverter(typeof(WalletTransactionStatusConverter))]
    public WalletTransactionStatus Status { get; set; }

    /// <summary>
    /// Error code for withdrawal failure, only returned when the type is "withdraw" and the state is "reject", "wallet-reject" and "failed".
    /// </summary>
    [JsonProperty("error-code")]
    public string ErrorCode { get; set; }

    /// <summary>
    /// Error description of withdrawal failure, only returned when the type is "withdraw" and the state is "reject", "wallet-reject" and "failed".
    /// </summary>
    [JsonProperty("error-msg")]
    public string ErrorMessage { get; set; }

    /// <summary>
    /// The timestamp in milliseconds for the transfer creation
    /// </summary>
    [JsonProperty("created-at"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// The timestamp in milliseconds for the transfer's latest update
    /// </summary>
    [JsonProperty("updated-at"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime UpdatedAt { get; set; }
}
