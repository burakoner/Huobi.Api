namespace Huobi.Api.Models.RestApi.Spot;

public class HuobiSpotKline
{
    /// <summary>
    /// The start time of the kline
    /// </summary>
    [JsonConverter(typeof(DateTimeConverter))]
    [JsonProperty("id")]
    public DateTime OpenTime { get; set; }

    /// <summary>
    /// The price at the opening
    /// </summary>
    [JsonProperty("open")]
    public decimal OpenPrice { get; set; }

    /// <summary>
    /// The highest price
    /// </summary>
    [JsonProperty("high")]
    public decimal HighPrice { get; set; }

    /// <summary>
    /// The lowest price
    /// </summary>
    [JsonProperty("low")]
    public decimal LowPrice { get; set; }

    /// <summary>
    /// The last price
    /// </summary>
    [JsonProperty("close")]
    public decimal ClosePrice { get; set; }

    /// <summary>
    /// The volume in base asset
    /// </summary>
    [JsonProperty("amount")]
    public decimal Volume { get; set; }

    /// <summary>
    /// The volume in quote asset (quantity * price)
    /// </summary>
    [JsonProperty("vol")]
    public decimal QuoteVolume { get; set; }

    /// <summary>
    /// The number of trades
    /// </summary>
    [JsonProperty("count")]
    public int TradeCount { get; set; }
}
