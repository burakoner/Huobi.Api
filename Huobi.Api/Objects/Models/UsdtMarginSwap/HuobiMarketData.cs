// .Converters;
using Huobi.Api.Models.RestApi.Spot;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Market data
    /// </summary>
    public class HuobiMarketData: HuobiSymbolData
    {
        /// <summary>
        /// Best ask
        /// </summary>
        [JsonConverter(typeof(ArrayConverter))]
        public HuobiSpotOrderBookEntry Ask { get; set; }
        /// <summary>
        /// Best bid
        /// </summary>
        [JsonConverter(typeof(ArrayConverter))]
        public HuobiSpotOrderBookEntry Bid { get; set; }
    }
}
