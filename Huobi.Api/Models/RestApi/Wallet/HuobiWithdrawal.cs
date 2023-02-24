namespace Huobi.Api.Models.RestApi.Wallet;

public class HuobiWithdrawal
{
    public long Id { get; set; }

    [JsonProperty("client-order-id")]
    public string ClientOrderId { get; set; }

    public string Type { get; set; }

    [JsonProperty("sub-type")]
    public string SubType { get; set; }

    public string Currency { get; set; }
    
    public string Chain { get; set; }

    [JsonProperty("tx-hash")]
    public string TransactionHash { get; set; }

    [JsonProperty("amount")]
    public decimal Quantity { get; set; }

    [JsonProperty("from-addr-tag")]
    public string FromAddressTag { get; set; }

    [JsonProperty("address")]
    public string Address { get; set; }

    [JsonProperty("address-tag")]
    public string AddressTag { get; set; }

    public decimal Fee { get; set; }

    [JsonProperty("state")]
    public string Status { get; set; }

    [JsonProperty("created-at")]
    public string CreatedAt { get; set; }

    [JsonProperty("updated-at")]
    public string UpdatedAt { get; set; }
}