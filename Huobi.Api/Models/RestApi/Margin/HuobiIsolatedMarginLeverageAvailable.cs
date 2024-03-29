﻿// .Converters;
using Newtonsoft.Json;

namespace Huobi.Api.Models.RestApi.Margin
{
    /// <summary>
    /// Available leverage info
    /// </summary>
    public class HuobiIsolatedMarginLeverageAvailable
    {
        /// <summary>
        /// Contract code
        /// </summary>
        [JsonProperty("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// Margin mode
        /// </summary>
        [JsonProperty("margin_mode")]
        [JsonConverter(typeof(EnumConverter))]
        public MarginMode MarginMode { get; set; }
        /// <summary>
        /// Available rates
        /// </summary>
        [JsonProperty("available_level_rate")]
        public string AvailableLevelRate { get; set; } = string.Empty;
    }
}
