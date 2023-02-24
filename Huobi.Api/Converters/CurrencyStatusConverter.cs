namespace Huobi.Api.Converters;

internal class CurrencyStatusConverter : BaseConverter<CurrencyStatus>
{
    public CurrencyStatusConverter() : this(true) { }
    public CurrencyStatusConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<CurrencyStatus, string>> Mapping => new List<KeyValuePair<CurrencyStatus, string>>
    {
        new KeyValuePair<CurrencyStatus, string>(CurrencyStatus.Allowed, "allowed"),
        new KeyValuePair<CurrencyStatus, string>(CurrencyStatus.Prohibited, "prohibited")
    };
}