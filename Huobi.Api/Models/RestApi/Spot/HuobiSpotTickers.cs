namespace Huobi.Api.Models.RestApi.Spot;

public abstract class HuobiSpotTickerBase
{
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

public class HuobiSpotTicker : HuobiSpotTickerBase
{
    /// <summary>
    /// The symbol
    /// </summary>
    public string Symbol { get; set; } = string.Empty;

    /// <summary>
    /// Best bid price
    /// </summary>
    [JsonProperty("bid")]
    public decimal BestBidPrice { get; set; }

    /// <summary>
    /// Quantity of the best bid
    /// </summary>
    [JsonProperty("bidSize")]
    public decimal BestBidQuantity { get; set; }

    /// <summary>
    /// Best ask price
    /// </summary>
    [JsonProperty("ask")]
    public decimal BestAskPrice { get; set; }

    /// <summary>
    /// Quantity of the best ask
    /// </summary>
    [JsonProperty("askSize")]
    public decimal BestAskQuantity { get; set; }
}

public class HuobiSpotMarketSummary: HuobiSpotTickerBase
{
    /// <summary>
    /// The id of the tick
    /// </summary>
    [JsonProperty("id")]
    public long Id { get; set; }

    /// <summary>
    /// Version
    /// </summary>
    public long Version { get; set; }
}

public class HuobiSpotAggregatedTicker : HuobiSpotMarketSummary
{
    /// <summary>
    /// The current best bid for the symbol
    /// </summary>
    [JsonProperty("bid")]
    public HuobiOrderBookEntry BestBid { get; set; }

    /// <summary>
    /// The current best ask for the symbol
    /// </summary>
    [JsonProperty("ask")]
    public HuobiOrderBookEntry BestAsk { get; set; }
}
