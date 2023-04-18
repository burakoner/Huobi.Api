// .Converters;
using Newtonsoft.Json;

namespace Huobi.Api.Models.RestApi.Margin
{
    /// <summary>
    /// Available leverage info
    /// </summary>
    public class HuobiCrossMarginLeverageAvailable : HuobiIsolatedMarginLeverageAvailable
    {
        /// <summary>
        /// Contract type
        /// </summary>
        [JsonProperty("contract_type")]
        [JsonConverter(typeof(EnumConverter))]
        public ContractType ContractType { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonProperty("pair")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Business type
        /// </summary>
        [JsonProperty("business_type")]
        [JsonConverter(typeof(EnumConverter))]
        public BusinessType BusinessType { get; set; }
    }
}
