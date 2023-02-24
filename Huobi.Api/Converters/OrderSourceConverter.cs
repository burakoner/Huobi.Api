namespace Huobi.Api.Converters;

internal class OrderSourceConverter : BaseConverter<OrderSource>
{
    public OrderSourceConverter() : this(true) { }
    public OrderSourceConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OrderSource, string>> Mapping => new List<KeyValuePair<OrderSource, string>>
    {
        new KeyValuePair<OrderSource, string>(OrderSource.Spot, "spot-api"),
        new KeyValuePair<OrderSource, string>(OrderSource.IsolatedMargin, "margin-api"),
        new KeyValuePair<OrderSource, string>(OrderSource.CrossMargin, "super-margin-api"),
        new KeyValuePair<OrderSource, string>(OrderSource.C2CMargin, "c2c-margin-api"),
    };
}
