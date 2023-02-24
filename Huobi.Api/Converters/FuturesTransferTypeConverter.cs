namespace Huobi.Api.Converters;

internal class FuturesTransferTypeConverter : BaseConverter<FuturesTransferType>
{
    public FuturesTransferTypeConverter() : this(true) { }
    public FuturesTransferTypeConverter(bool useQuotes) : base(useQuotes) { }

    protected override List<KeyValuePair<FuturesTransferType, string>> Mapping => new List<KeyValuePair<FuturesTransferType, string>>
    {
        new KeyValuePair<FuturesTransferType, string>(FuturesTransferType.FuturesToPro, "futures-to-pro"),
        new KeyValuePair<FuturesTransferType, string>(FuturesTransferType.ProToFutures, "pro-to-futures"),
    };
}
