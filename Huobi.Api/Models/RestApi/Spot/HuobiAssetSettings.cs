namespace Huobi.Api.Models.RestApi.Spot;

public class HuobiAssetSettings
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("dn")]
    public string DisplayName { get; set; }

    [JsonProperty("vat"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime VisibleTime { get; set; }

    [JsonProperty("det"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime DepositEnableTime { get; set; }

    [JsonProperty("wet"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime WithdrawalEnableTime { get; set; }

    [JsonProperty("wp")]
    public int WithdrawPrecision { get; set; }

    [JsonProperty("ct"), JsonConverter(typeof(CurrencyTypeConverter))]
    public CurrencyFeeType FeeType { get; set; }

    [JsonProperty("cp")]
    public string CurrencyPartition { get; set; }

    [JsonProperty("ss")]
    public IEnumerable<string> SupportedSites { get; set; }

    [JsonProperty("oe")]
    public bool IsOtcEnabled { get; set; }

    [JsonProperty("dma")]
    public decimal MinimumDepositQuantity { get; set; }

    [JsonProperty("wma")]
    public decimal MinimumWithdrawalQuantity { get; set; }

    [JsonProperty("sp")]
    public int DisplayPrecision { get; set; }

    /// <summary>
    /// Weight sort
    /// </summary>
    [JsonProperty("w")]
    public int Weight { get; set; }

    [JsonProperty("qc")]
    public bool IsQuoteCurrency { get; set; }

    [JsonProperty("state"), JsonConverter(typeof(SymbolStatusConverter))]
    public SymbolStatus Status { get; set; }

    [JsonProperty("v")]
    public bool IsVisible { get; set; }

    [JsonProperty("whe")]
    public bool IsWhiteEnabled { get; set; }

    [JsonProperty("cd")]
    public bool IsCountryDisabled { get; set; }

    [JsonProperty("de")]
    public bool IsDepositEnabled { get; set; }

    [JsonProperty("we")]
    public bool IsWithdrawalEnabled { get; set; }

    [JsonProperty("cawt")]
    public bool CurrencyAddressHasTag { get; set; }

    [JsonProperty("cao")]
    public bool? CurrencyAddressOneOff { get; set; }

    [JsonProperty("fc")]
    public int FastConfirms { get; set; }

    [JsonProperty("sc")]
    public int SafeConfirms { get; set; }

    [JsonProperty("wd")]
    public string WithdrawalDescription { get; set; }

    [JsonProperty("swd")]
    public string WithdrawalSuspensionDescription { get; set; }

    [JsonProperty("dd")]
    public string DepositDescription { get; set; }

    [JsonProperty("sdd")]
    public string DepositSuspensionDescription { get; set; }

    [JsonProperty("svd")]
    public string SuspensionVisibleDescription { get; set; }

    [JsonProperty("tags")]
    public string Tags { get; set; }

    [JsonProperty("fn")]
    public string FullName { get; set; }

    [JsonProperty("bc")]
    public string Blockchains { get; set; }

    //[JsonProperty("iqc")]
    //public bool IsQuoteCurrency { get; set; }
}