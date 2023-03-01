namespace Huobi.Api.Converters;

internal class SubUserStatusConverter : BaseConverter<SubUserStatus>
{
    public SubUserStatusConverter() : this(true) { }
    public SubUserStatusConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<SubUserStatus, string>> Mapping => new List<KeyValuePair<SubUserStatus, string>>
    {
        new KeyValuePair<SubUserStatus, string>(SubUserStatus.Locked, "lock"),
        new KeyValuePair<SubUserStatus, string>(SubUserStatus.Normal, "normal")
    };
}
