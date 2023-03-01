namespace Huobi.Api.Models.RestApi.Spot.Trading;

public class HuobiSpotTrade
{
    /// <summary>
    /// The id of the update
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// The timestamp of the trade
    /// </summary>
    [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// The id of the trade
    /// </summary>
    [JsonProperty("trade-id")]
    public long TradeId { get; set; }

    // Rest uses trade-id, socket uses tradeId
    [JsonProperty("tradeId")]
    private long TradeIdAlias { get => TradeId; set => TradeId = value; }

    /// <summary>
    /// The price of the trade
    /// </summary>
    public decimal Price { get; set; }
    /// <summary>
    /// The quantity of the trade
    /// </summary>
    [JsonProperty("amount")]
    public decimal Quantity { get; set; }

    /// <summary>
    /// The side of the trade
    /// </summary>
    [JsonProperty("direction"), JsonConverter(typeof(SpotOrderSideConverter))]
    public SpotOrderSide Side { get; set; }
}
