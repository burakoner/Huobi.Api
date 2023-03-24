using Newtonsoft.Json;

namespace Huobi.Api.Models.RestApi.Futures
{
    /// <summary>
    /// Placed order id 
    /// </summary>
    public class HuobiPlacedOrderId
    {
        /// <summary>
        /// Order id
        /// </summary>
        [JsonProperty("order_id")]
        public long OrderId { get; set; }
        /// <summary>
        /// Client order id
        /// </summary>
        [JsonProperty("client_order_id")]
        public long? ClientOrderId { get; set; }
    }
}
