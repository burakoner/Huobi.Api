namespace Huobi.Api.Converters;

internal class KlineIntervalConverter : BaseConverter<KlineInterval>
{
    public KlineIntervalConverter() : this(true) { }
    public KlineIntervalConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<KlineInterval, string>> Mapping => new List<KeyValuePair<KlineInterval, string>>
    {
        new KeyValuePair<KlineInterval, string>(KlineInterval.OneMinute, "1min"),
        new KeyValuePair<KlineInterval, string>(KlineInterval.FiveMinutes, "5min"),
        new KeyValuePair<KlineInterval, string>(KlineInterval.FifteenMinutes, "15min"),
        new KeyValuePair<KlineInterval, string>(KlineInterval.ThirtyMinutes, "30min"),
        new KeyValuePair<KlineInterval, string>(KlineInterval.OneHour, "60min"),
        new KeyValuePair<KlineInterval, string>(KlineInterval.FourHours, "4hour"),
        new KeyValuePair<KlineInterval, string>(KlineInterval.OneDay, "1day"),
        new KeyValuePair<KlineInterval, string>(KlineInterval.OneWeek, "1week"),
        new KeyValuePair<KlineInterval, string>(KlineInterval.OneMonth, "1mon"),
        new KeyValuePair<KlineInterval, string>(KlineInterval.OneYear, "1year")
    };
}
