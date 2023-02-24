using Huobi.Api.Models.RestApi.Spot;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Socket
{
    /// <summary>
    /// Best offer update
    /// </summary>
    public class HuobiBestOfferUpdate
    {
        /// <summary>
        /// Order id
        /// </summary>
        [JsonProperty("mrid")]
        public long OrderId { get; set; }
        /// <summary>
        /// Update id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Best bid
        /// </summary>
        public HuobiSpotOrderBookEntry Bid { get; set; } = null!;
        /// <summary>
        /// Best ask
        /// </summary>
        public HuobiSpotOrderBookEntry Ask { get; set; } = null!;
    }
}
