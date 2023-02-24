namespace Huobi.Api.Converters;

internal class WalletTransactionStatusConverter : BaseConverter<WalletTransactionStatus>
{
    public WalletTransactionStatusConverter() : this(true) { }
    public WalletTransactionStatusConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<WalletTransactionStatus, string>> Mapping => new List<KeyValuePair<WalletTransactionStatus, string>>
    {
        new KeyValuePair<WalletTransactionStatus, string>(WalletTransactionStatus.Verifying, "verifying"),
        new KeyValuePair<WalletTransactionStatus, string>(WalletTransactionStatus.Failed, "failed"),
        new KeyValuePair<WalletTransactionStatus, string>(WalletTransactionStatus.Submitted, "submitted"),
        new KeyValuePair<WalletTransactionStatus, string>(WalletTransactionStatus.Reexamine, "reexamine"),
        new KeyValuePair<WalletTransactionStatus, string>(WalletTransactionStatus.Canceled, "canceled"),
        new KeyValuePair<WalletTransactionStatus, string>(WalletTransactionStatus.Pass, "pass"),
        new KeyValuePair<WalletTransactionStatus, string>(WalletTransactionStatus.Reject, "reject"),
        new KeyValuePair<WalletTransactionStatus, string>(WalletTransactionStatus.PreTransfer, "pre-transfer"),
        new KeyValuePair<WalletTransactionStatus, string>(WalletTransactionStatus.WalletTransfer, "wallet-transfer"),
        new KeyValuePair<WalletTransactionStatus, string>(WalletTransactionStatus.WalletReject, "wallet-reject"),
        new KeyValuePair<WalletTransactionStatus, string>(WalletTransactionStatus.Confirmed, "confirmed"),
        new KeyValuePair<WalletTransactionStatus, string>(WalletTransactionStatus.ConfirmError, "confirm-error"),
        new KeyValuePair<WalletTransactionStatus, string>(WalletTransactionStatus.Repealed, "repealed"),
        new KeyValuePair<WalletTransactionStatus, string>(WalletTransactionStatus.Unknown, "unknown"),
        new KeyValuePair<WalletTransactionStatus, string>(WalletTransactionStatus.Confirming, "confirming"),
        new KeyValuePair<WalletTransactionStatus, string>(WalletTransactionStatus.Safe, "safe"),
        new KeyValuePair<WalletTransactionStatus, string>(WalletTransactionStatus.Orphan, "orphan"),
    };
}
