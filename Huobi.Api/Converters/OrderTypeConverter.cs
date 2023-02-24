namespace Huobi.Api.Converters;

internal class OrderTypeConverter : BaseConverter<SpotOrderType>
{
    public OrderTypeConverter() : this(true) { }
    public OrderTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<SpotOrderType, string>> Mapping => new List<KeyValuePair<SpotOrderType, string>>
    {
        new KeyValuePair<SpotOrderType, string>(SpotOrderType.Limit, "limit"),
        new KeyValuePair<SpotOrderType, string>(SpotOrderType.Market, "market"),
        new KeyValuePair<SpotOrderType, string>(SpotOrderType.IOC, "ioc"),
        new KeyValuePair<SpotOrderType, string>(SpotOrderType.LimitMaker, "limit-maker"),
        new KeyValuePair<SpotOrderType, string>(SpotOrderType.StopLimit, "stop-limit"),
        new KeyValuePair<SpotOrderType, string>(SpotOrderType.FillOrKillLimit, "limit-fok"),
        new KeyValuePair<SpotOrderType, string>(SpotOrderType.FillOrKillStopLimit, "stop-limit-fok"),

        new KeyValuePair<SpotOrderType, string>(SpotOrderType.Limit, "buy-limit"),
        new KeyValuePair<SpotOrderType, string>(SpotOrderType.Limit, "sell-limit"),
        new KeyValuePair<SpotOrderType, string>(SpotOrderType.Market, "buy-market"),
        new KeyValuePair<SpotOrderType, string>(SpotOrderType.Market, "sell-market"),
        new KeyValuePair<SpotOrderType, string>(SpotOrderType.IOC, "buy-ioc"),
        new KeyValuePair<SpotOrderType, string>(SpotOrderType.IOC, "sell-ioc"),
        new KeyValuePair<SpotOrderType, string>(SpotOrderType.LimitMaker, "buy-limit-maker"),
        new KeyValuePair<SpotOrderType, string>(SpotOrderType.LimitMaker, "sell-limit-maker"),
        new KeyValuePair<SpotOrderType, string>(SpotOrderType.StopLimit, "buy-stop-limit"),
        new KeyValuePair<SpotOrderType, string>(SpotOrderType.StopLimit, "sell-stop-limit"),

        new KeyValuePair<SpotOrderType, string>(SpotOrderType.FillOrKillLimit, "buy-limit-fok"),
        new KeyValuePair<SpotOrderType, string>(SpotOrderType.FillOrKillLimit, "sell-limit-fok"),
        new KeyValuePair<SpotOrderType, string>(SpotOrderType.FillOrKillStopLimit, "buy-stop-limit-fok"),
        new KeyValuePair<SpotOrderType, string>(SpotOrderType.FillOrKillStopLimit, "sell-stop-limit-fok")
    };
}
