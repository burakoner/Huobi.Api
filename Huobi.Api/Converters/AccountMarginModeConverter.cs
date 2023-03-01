namespace Huobi.Api.Converters;

internal class AccountMarginModeConverter : BaseConverter<AccountMarginMode>
{
    public AccountMarginModeConverter() : this(true) { }
    public AccountMarginModeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<AccountMarginMode, string>> Mapping => new List<KeyValuePair<AccountMarginMode, string>>
    {
        new KeyValuePair<AccountMarginMode, string>(AccountMarginMode.CrossMargin, "cross-margin"),
        new KeyValuePair<AccountMarginMode, string>(AccountMarginMode.IsolatedMargin, "isolated-margin"),
    };
}