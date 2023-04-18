namespace Huobi.Api.Models.RestApi.Spot;

public class HuobiBlockchainInfo
{
    [JsonProperty("adt")]
    public bool DepositAddressHasTag { get; set; }

    [JsonProperty("ac")]
    public string AddressOfChain { get; set; }

    [JsonProperty("ao")]
    public bool AddressOneOff { get; set; }

    [JsonProperty("awt")]
    public bool AddressWithTag { get; set; }

    [JsonProperty("chain")]
    public string Chain { get; set; }

    /// <summary>
    /// chain type: plain, live, old, new, legal, tooold
    /// </summary>
    [JsonProperty("ct")]
    public string Type { get; set; }

    [JsonProperty("currency")]
    public string Currency { get; set; }

    [JsonProperty("deposit-desc")]
    public string DepositDescription { get; set; }

    [JsonProperty("de")]
    public bool DepositEnabled { get; set; }

    [JsonProperty("dma")]
    public decimal DepositMinimumQuantity { get; set; }

    [JsonProperty("deposit-tips-desc")]
    public string DepositTipsDescription { get; set; }

    [JsonProperty("dn")]
    public string DisplayName { get; set; }

    [JsonProperty("fc")]
    public int FastConfirms { get; set; }

    [JsonProperty("sc")]
    public int SafeConfirms { get; set; }

    [JsonProperty("ft")]
    public string FeeType { get; set; }

    [JsonProperty("default")]
    public bool IsDefault { get; set; }

    [JsonProperty("replace-chain-info-desc")]
    public string ReplaceChainInfoDescription { get; set; }

    [JsonProperty("replace-chain-notification-desc")]
    public string ReplaceChainNotificationDescription { get; set; }

    [JsonProperty("replace-chain-popup-desc")]
    public string ReplaceChainPopupDescription { get; set; }

    [JsonProperty("sda")]
    public string SuspendDepositAnnouncement { get; set; }

    [JsonProperty("suspend-deposit-desc")]
    public string SuspendDepositDescription { get; set; }

    [JsonProperty("swa")]
    public string SuspendWithdrawalAnnouncement { get; set; }

    [JsonProperty("suspend-withdraw-desc")]
    public string SuspendWithdrawalDescription { get; set; }

    [JsonProperty("v")]
    public bool IsVisible { get; set; }

    [JsonProperty("withdraw-desc")]
    public string WithdrawalDecsription { get; set; }

    [JsonProperty("we")]
    public bool WithdrawalEnabled { get; set; }

    [JsonProperty("wma")]
    public decimal WithdrawalMinimumQuantity { get; set; }

    [JsonProperty("wp")]
    public int WithdrawalPrecision { get; set; }

    [JsonProperty("fn")]
    public string FullName { get; set; }

    [JsonProperty("withdraw-tips-desc")]
    public string WithdrawalTipsDescription { get; set; }

    [JsonProperty("suspend-visible-desc")]
    public string SuspendVisibleDescription { get; set; }

}
