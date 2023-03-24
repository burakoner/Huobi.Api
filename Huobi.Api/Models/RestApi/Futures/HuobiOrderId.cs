using Newtonsoft.Json;

namespace Huobi.Api.Models.RestApi.Futures
{
    /// <summary>
    /// Order id
    /// </summary>
    public class HuobiOrderId
    {
        /// <summary>
        /// Order id
        /// </summary>
        [JsonProperty("order_id")]
        public string OrderId { get; set; } = string.Empty;
    }
}
