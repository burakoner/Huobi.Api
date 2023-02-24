namespace Huobi.Api.Converters;

internal class CurrencyTypeConverter : BaseConverter<CurrencyType>
{
    public CurrencyTypeConverter() : this(true) { }
    public CurrencyTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<CurrencyType, string>> Mapping => new()
    {
        new KeyValuePair<CurrencyType, string>(CurrencyType.Virtual, "1"),
        new KeyValuePair<CurrencyType, string>(CurrencyType.Fiat, "2"),
    };
}