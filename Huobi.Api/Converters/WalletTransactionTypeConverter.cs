namespace Huobi.Api.Converters;

internal class WalletTransactionTypeConverter : BaseConverter<WalletTransactionType>
{
    public WalletTransactionTypeConverter() : this(true) { }
    public WalletTransactionTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<WalletTransactionType, string>> Mapping => new List<KeyValuePair<WalletTransactionType, string>>
    {
        new KeyValuePair<WalletTransactionType, string>(WalletTransactionType.Deposit, "deposit"),
        new KeyValuePair<WalletTransactionType, string>(WalletTransactionType.Withdraw, "withdraw"),
    };
}
