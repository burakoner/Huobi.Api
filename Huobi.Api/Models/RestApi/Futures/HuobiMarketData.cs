using Huobi.Api.Models.RestApi.Spot;
using Huobi.Api.Models.StreamApi.Spot;

namespace Huobi.Api.Models.RestApi.Futures;

/// <summary>
/// Market data
/// </summary>
public class HuobiMarketData : HuobiSymbolData
{
    /// <summary>
    /// Best ask
    /// </summary>
    [JsonConverter(typeof(ArrayConverter))]
    public HuobiOrderBookEntry Ask { get; set; }
    /// <summary>
    /// Best bid
    /// </summary>
    [JsonConverter(typeof(ArrayConverter))]
    public HuobiOrderBookEntry Bid { get; set; }
}
