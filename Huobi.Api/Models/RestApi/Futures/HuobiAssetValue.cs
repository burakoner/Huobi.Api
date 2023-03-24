using Newtonsoft.Json;

namespace Huobi.Api.Models.RestApi.Futures
{
    /// <summary>
    /// Asset valuation
    /// </summary>
    public class HuobiAssetValue
    {
        /// <summary>
        /// Asset name
        /// </summary>
        [JsonProperty("valuation_asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Balance
        /// </summary>
        public decimal Balance { get; set; }
    }
}
