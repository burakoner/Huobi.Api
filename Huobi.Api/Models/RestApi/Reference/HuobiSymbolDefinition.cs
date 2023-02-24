namespace Huobi.Api.Models.RestApi.Reference;

public class HuobiSymbolDefinition
{
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("bc")]
    public string BaseCurrency { get; set; }

    [JsonProperty("qc")]
    public string QuoteCurrency { get; set; }

    [JsonProperty("state"), JsonConverter(typeof(SymbolStatusConverter))]
    public SymbolStatus Status { get; set; }

    [JsonProperty("sp")]
    public string SymbolPartition { get; set; }

    [JsonProperty("tags")]
    public string Tags { get; set; } = string.Empty;

    [JsonProperty("lr")]
    public decimal? LeverageRatio { get; set; }

    [JsonProperty("smlr")]
    public decimal? SuperMarginLeverageRatio { get; set; }

    [JsonProperty("pp")]
    public int PricePrecision { get; set; }

    [JsonProperty("ap")]
    public int QuantityPrecision { get; set; }

    [JsonProperty("vp")]
    public int ValuePrecision { get; set; }

    [JsonProperty("minoa")]
    public decimal MinimumOrderQuantity { get; set; }

    [JsonProperty("maxoa")]
    public decimal MaximumOrderQuantity { get; set; }

    [JsonProperty("minov")]
    public decimal MinimumOrderValue { get; set; }

    [JsonProperty("lominoa")]
    public decimal LimitOrderMinimumOrderQuantity { get; set; }

    [JsonProperty("lomaxoa")]
    public decimal LimitOrderMaximumOrderQuantity { get; set; }

    [JsonProperty("lomaxba")]
    public decimal LimitOrderMaximumBuyQuantity { get; set; }

    [JsonProperty("lomaxsa")]
    public decimal LimitOrderMaximumSellQuantity { get; set; }

    [JsonProperty("smminoa")]
    public decimal MarketOrderMinimumOrderQuantity { get; set; }

    [JsonProperty("smmaxoa")]
    public decimal MarketOrderMaximumOrderQuantity { get; set; }

    [JsonProperty("bmmaxov")]
    public decimal MarketOrderMaximumBuyOrderValue { get; set; }

    [JsonProperty("blmlt")]
    public decimal BuyLimitMustLessThan { get; set; }

    [JsonProperty("slmgt")]
    public decimal SellLimitMustLessThan { get; set; }

    [JsonProperty("msormlt")]
    public decimal MarketSellOrderRateMustLessThan { get; set; }

    [JsonProperty("mbormlt")]
    public decimal MarketBuyOrderRateMustLessThan { get; set; }

    [JsonProperty("maxov")]
    public decimal MaximumOrderValue { get; set; }

    [JsonProperty("at")]
    public string ApiTrading { get; set; }

    [JsonProperty("u")]
    public string Underlying { get; set; }

    [JsonProperty("mfr")]
    public decimal? mgmtFeeRate { get; set; }

    [JsonProperty("ct"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? ChargeTime { get; set; }

    [JsonProperty("rt"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? RebalTime { get; set; }

    [JsonProperty("rebal-threshold")]
    public decimal? RebalThreshold { get; set; }

    [JsonProperty("init-nav")]
    public decimal? InitNav { get; set; }

    [JsonProperty("flr")]
    public decimal? FundingLeverageRatio { get; set; }

    [JsonProperty("castate")]
    public string CallAuctionStatus { get; set; }
}
