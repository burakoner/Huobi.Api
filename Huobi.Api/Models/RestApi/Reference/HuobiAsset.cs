namespace Huobi.Api.Models.RestApi.Reference;

public class HuobiAsset
{
    [JsonProperty("cc")]
    public string Asset { get; set; }

    [JsonProperty("dn")]
    public string DisplayName { get; set; }

    [JsonProperty("fn")]
    public string FullName { get; set; }

    [JsonProperty("at"), JsonConverter(typeof(CurrencyTypeConverter))]
    public CurrencyType Type { get; set; }

    [JsonProperty("wp")]
    public int WithdrawPrecision { get; set; }

    [JsonProperty("ft"), JsonConverter(typeof(CurrencyFeeTypeConverter))]
    public CurrencyFeeType? FeeType { get; set; }

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

    [JsonProperty("wed")]
    public bool IsWithdrawalEnabled { get; set; }

    [JsonProperty("cawt")]
    public bool CurrencyAddressHasTag { get; set; }

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

    [JsonProperty("bc")]
    public string Blockchains { get; set; }
}
