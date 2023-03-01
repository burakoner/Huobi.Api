namespace Huobi.Api.Models.RestApi.Spot.Public;

public class HuobiSymbolSettings
{
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("sn")]
    public string SymbolName { get; set; }

    [JsonProperty("bc")]
    public string BaseCurrency { get; set; }

    [JsonProperty("qc")]
    public string QuoteCurrency { get; set; }

    [JsonProperty("state"), JsonConverter(typeof(SymbolStatusConverter))]
    public SymbolStatus Status { get; set; }

    [JsonProperty("ve")]
    public bool IsVisibleEnabled { get; set; }

    [JsonProperty("we")]
    public bool IsWhiteEnabled { get; set; }

    [JsonProperty("dl")]
    public bool IsDelisted { get; set; }

    [JsonProperty("cd")]
    public bool IsCountryDisabled { get; set; }

    [JsonProperty("te")]
    public bool IsTradeEnabled { get; set; }

    [JsonProperty("ce")]
    public bool IsCancelEnabled { get; set; }

    [JsonProperty("tet"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime TradeEnabledAt { get; set; }

    [JsonProperty("toa"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime TradeOpenAt { get; set; }

    [JsonProperty("tca"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime TradeClosedAt { get; set; }

    [JsonProperty("voa"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime VisibleOpenAt { get; set; }

    [JsonProperty("vca"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime VisibleClosedAt { get; set; }

    [JsonProperty("sp")]
    public string SymbolPartition { get; set; }

    [JsonProperty("tm")]
    public string TradeMarket { get; set; }

    [JsonProperty("w")]
    public int Weight { get; set; }

    [JsonProperty("ttp")]
    public int TradeTotalPrecision { get; set; }

    [JsonProperty("tap")]
    public int TradeQuantityPrecision { get; set; }

    [JsonProperty("tpp")]
    public int TradePricePrecision { get; set; }

    [JsonProperty("fp")]
    public int FeePrecision { get; set; }

    [JsonProperty("tags")]
    public string Tags { get; set; } = string.Empty;

    [JsonProperty("d"), JsonConverter(typeof(SymbolDirectionConverter))]
    public SymbolDirection? Direction { get; set; }

    [JsonProperty("bcdn")]
    public string BaseCurrencyDisplayName { get; set; }

    [JsonProperty("qcdn")]
    public string QuoteCurrencyDisplayName { get; set; }

    [JsonProperty("elr")]
    public decimal? EtpLeverageRatio { get; set; }

    [JsonProperty("suspend_desc")]
    public string SuspendDescription { get; set; }

    [JsonProperty("castate")]
    public string CallAuctionStatus { get; set; }

    [JsonProperty("ca1oa"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? CallAuction1OpenAt { get; set; }

    [JsonProperty("ca1ca"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? CallAuction1ClosedAt { get; set; }

    [JsonProperty("ca2oa"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? CallAuction2OpenAt { get; set; }

    [JsonProperty("ca2ca"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime? CallAuction2ClosedAt { get; set; }
}
