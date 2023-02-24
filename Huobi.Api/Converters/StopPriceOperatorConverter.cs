namespace Huobi.Api.Converters;

internal class StopPriceOperatorConverter : BaseConverter<StopPriceOperator>
{
    public StopPriceOperatorConverter() : this(true) { }
    public StopPriceOperatorConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<StopPriceOperator, string>> Mapping => new List<KeyValuePair<StopPriceOperator, string>>
    {
        new KeyValuePair<StopPriceOperator, string>(StopPriceOperator.LesserThanOrEqual, "lte"),
        new KeyValuePair<StopPriceOperator, string>(StopPriceOperator.GreaterThanOrEqual, "gte")
    };
}
