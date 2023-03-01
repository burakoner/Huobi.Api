namespace Huobi.Api.Converters;

internal class SubUserStatusActionConverter : BaseConverter<SubUserStatusAction>
{
    public SubUserStatusActionConverter() : this(true) { }
    public SubUserStatusActionConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<SubUserStatusAction, string>> Mapping => new List<KeyValuePair<SubUserStatusAction, string>>
    {
        new KeyValuePair<SubUserStatusAction, string>(SubUserStatusAction.Lock, "lock"),
        new KeyValuePair<SubUserStatusAction, string>(SubUserStatusAction.Unlock, "unlock")
    };
}
