namespace Huobi.Api.Enums;

public enum CurrencyFeeType 
{
    [Map("eth")]
    Fixed,

    [Map("btc")]
    Interval,

    [Map("husd")]
    Proportional,
}
