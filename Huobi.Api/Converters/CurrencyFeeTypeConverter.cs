namespace Huobi.Api.Converters;

internal class CurrencyFeeTypeConverter : BaseConverter<CurrencyFeeType>
{
    public CurrencyFeeTypeConverter() : this(true) { }
    public CurrencyFeeTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<CurrencyFeeType, string>> Mapping => new()
    {
        new KeyValuePair<CurrencyFeeType, string>(CurrencyFeeType.Fixed, "eth"),
        new KeyValuePair<CurrencyFeeType, string>(CurrencyFeeType.Interval, "btc"),
        new KeyValuePair<CurrencyFeeType, string>(CurrencyFeeType.Proportional, "husd"),
    };
}