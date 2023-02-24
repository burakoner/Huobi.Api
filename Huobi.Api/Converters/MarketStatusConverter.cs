namespace Huobi.Api.Converters;

internal class MarketStatusConverter : BaseConverter<MarketStatus>
{
    public MarketStatusConverter() : this(true) { }
    public MarketStatusConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<MarketStatus, string>> Mapping => new()
    {
        new KeyValuePair<MarketStatus, string>(MarketStatus.Normal, "1"),
        new KeyValuePair<MarketStatus, string>(MarketStatus.Halted, "2"),
        new KeyValuePair<MarketStatus, string>(MarketStatus.CancelOnly, "3"),
    };
}