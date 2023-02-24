namespace Huobi.Api.Converters;

internal class SpotOrderSideConverter : BaseConverter<SpotOrderSide>
{
    public SpotOrderSideConverter() : this(true) { }
    public SpotOrderSideConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<SpotOrderSide, string>> Mapping => new()
    {
        new KeyValuePair<SpotOrderSide, string>(SpotOrderSide.Buy, "buy"),
        new KeyValuePair<SpotOrderSide, string>(SpotOrderSide.Sell, "sell"),

        new KeyValuePair<SpotOrderSide, string>(SpotOrderSide.Buy, "buy-market"),
        new KeyValuePair<SpotOrderSide, string>(SpotOrderSide.Sell, "sell-market"),
        new KeyValuePair<SpotOrderSide, string>(SpotOrderSide.Buy, "buy-limit"),
        new KeyValuePair<SpotOrderSide, string>(SpotOrderSide.Sell, "sell-limit"),
        new KeyValuePair<SpotOrderSide, string>(SpotOrderSide.Buy, "buy-ioc"),
        new KeyValuePair<SpotOrderSide, string>(SpotOrderSide.Sell, "sell-ioc"),
        new KeyValuePair<SpotOrderSide, string>(SpotOrderSide.Buy, "buy-limit-maker,"),
        new KeyValuePair<SpotOrderSide, string>(SpotOrderSide.Sell, "sell-limit-maker"),
        new KeyValuePair<SpotOrderSide, string>(SpotOrderSide.Buy, "buy-stop-limit"),
        new KeyValuePair<SpotOrderSide, string>(SpotOrderSide.Sell, "sell-stop-limit"),
        new KeyValuePair<SpotOrderSide, string>(SpotOrderSide.Buy, "buy-limit-fok"),
        new KeyValuePair<SpotOrderSide, string>(SpotOrderSide.Sell, "sell-limit-fok"),
        new KeyValuePair<SpotOrderSide, string>(SpotOrderSide.Buy, "buy-stop-limit-fok"),
        new KeyValuePair<SpotOrderSide, string>(SpotOrderSide.Sell, "sell-stop-limit-fok"),
    };
}
