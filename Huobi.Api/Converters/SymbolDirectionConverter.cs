namespace Huobi.Api.Converters;

internal class SymbolDirectionConverter : BaseConverter<SymbolDirection>
{
    public SymbolDirectionConverter() : this(true) { }
    public SymbolDirectionConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<SymbolDirection, string>> Mapping => new()
    {
        new KeyValuePair<SymbolDirection, string>(SymbolDirection.Long, "1"),
        new KeyValuePair<SymbolDirection, string>(SymbolDirection.Short, "2"),
    };
}