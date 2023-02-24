namespace Huobi.Api.Converters;

internal class SymbolStatusConverter : BaseConverter<SymbolStatus>
{
    public SymbolStatusConverter() : this(true) { }
    public SymbolStatusConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<SymbolStatus, string>> Mapping => new()
    {
        new KeyValuePair<SymbolStatus, string>(SymbolStatus.Unknown, "unknown"),
        new KeyValuePair<SymbolStatus, string>(SymbolStatus.NotOnline, "not-online"),
        new KeyValuePair<SymbolStatus, string>(SymbolStatus.PreOnline, "pre-online"),
        new KeyValuePair<SymbolStatus, string>(SymbolStatus.Online, "online"),
        new KeyValuePair<SymbolStatus, string>(SymbolStatus.Suspended, "suspend"),
        new KeyValuePair<SymbolStatus, string>(SymbolStatus.Offline, "offline"),
        new KeyValuePair<SymbolStatus, string>(SymbolStatus.TransferBoard, "transfer-board"),
        new KeyValuePair<SymbolStatus, string>(SymbolStatus.Fuse, "fuse"),
    };
}