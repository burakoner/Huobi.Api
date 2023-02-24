namespace Huobi.Api.Converters;

internal class InstrumentStatusConverter : BaseConverter<InstrumentStatus>
{
    public InstrumentStatusConverter() : this(true) { }
    public InstrumentStatusConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<InstrumentStatus, string>> Mapping => new()
    {
        new KeyValuePair<InstrumentStatus, string>(InstrumentStatus.Normal, "normal"),
        new KeyValuePair<InstrumentStatus, string>(InstrumentStatus.Delisted, "delisted"),
    };
}