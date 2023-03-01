namespace Huobi.Api.Models.RestApi.Spot.Public;

/// <summary>
/// Symbol data
/// </summary>
public class HuobiSymbol
{
    /// <summary>
    /// The symbol name
    /// </summary>
    [JsonProperty("sc")]
    public string Symbol { get; set; }

    [JsonProperty("dn")]
    public string DisplayName { get; set; }

    /// <summary>
    /// Leverage status of symbol
    /// </summary>
    [JsonProperty("si"), JsonConverter(typeof(SymbolStatusConverter))]
    public SymbolStatus IsolatedMarginStatus { get; set; }

    /// <summary>
    /// Full leverage status of symbol
    /// </summary>
    [JsonProperty("scr"), JsonConverter(typeof(SymbolStatusConverter))]
    public SymbolStatus CrossMarginStatus { get; set; }

    [JsonProperty("bc")]
    public string BaseCurrency { get; set; }

    [JsonProperty("bcdn")]
    public string BaseCurrencyDisplayName { get; set; }

    [JsonProperty("qc")]
    public string QuoteCurrency { get; set; }

    [JsonProperty("qcdn")]
    public string QuoteCurrencyDisplayName { get; set; }

    /// <summary>
    /// The state of the symbol
    /// </summary>
    [JsonProperty("state"), JsonConverter(typeof(SymbolStatusConverter))]
    public SymbolStatus Status { get; set; }

    [JsonProperty("whe")]
    public bool IsWhiteEnabled { get; set; }

    [JsonProperty("cd")]
    public bool IsCountryDisabled { get; set; }

    [JsonProperty("te")]
    public bool IsTradeEnabled { get; set; }

    [JsonProperty("toa"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime TradeOpenAt { get; set; }

    [JsonProperty("sp")]
    public string SymbolPartition { get; set; }

    /// <summary>
    /// Weight sort
    /// </summary>
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

    [JsonProperty("suspend_desc")]
    public string SuspendDescription { get; set; }

    [JsonProperty("transfer_board_desc")]
    public string TransferBoardDescription { get; set; }

    /// <summary>
    /// Tags, multiple tags are separated by commas, such as: st, hadax
    /// </summary>
    [JsonProperty("tags")]
    public string Tags { get; set; } = string.Empty;

    [JsonProperty("lr")]
    public decimal? LeverageRatio { get; set; }

    [JsonProperty("sm")]
    public bool SuperMargin { get; set; }

    [JsonProperty("smlr")]
    public decimal? SuperMarginLeverageRatio { get; set; }

    [JsonProperty("flr")]
    public decimal? FundingLeverageRatio { get; set; }

    /// <summary>
    /// withdraw_risk, such as: 3, or null if the symbol does not support super-margin
    /// </summary>
    [JsonProperty("wr")]
    public decimal? WithdrawRisk { get; set; }

    [JsonProperty("d"), JsonConverter(typeof(SymbolDirectionConverter))]
    public SymbolDirection? Direction { get; set; }

    [JsonProperty("elr")]
    public decimal? EtpLeverageRatio { get; set; }

    //[JsonProperty("p")]
    //public partitions { get; set; }
}
