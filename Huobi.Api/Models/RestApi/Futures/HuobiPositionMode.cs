// .Converters;
using Newtonsoft.Json;

namespace Huobi.Api.Models.RestApi.Futures
{
    /// <summary>
    /// Position mode
    /// </summary>
    public class HuobiPositionMode
    {
        /// <summary>
        /// Margin account
        /// </summary>
        [JsonProperty("margin_account")]
        public string MarginAccount { get; set; } = string.Empty;
        /// <summary>
        /// Position mode
        /// </summary>
        [JsonProperty("position_mode")]
        [JsonConverter(typeof(EnumConverter))]
        public PositionMode PositionMode { get; set; }
    }
}
