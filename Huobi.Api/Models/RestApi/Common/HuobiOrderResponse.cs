namespace Huobi.Api.Models.RestApi.Common;

public class HuobiOrderResponse
{
    [JsonIgnore]
    public bool Success { get => OrderId.HasValue; }

    [JsonProperty("order-id")]
    public long? OrderId { get; set; }

    [JsonProperty("client-order-id")]
    public string ClientOrderId { get; set; }

    [JsonProperty("err-code")]
    public string ErrorCode { get; set; }

    [JsonProperty("err-code")]
    public string ErrorMessage { get; set; }
}