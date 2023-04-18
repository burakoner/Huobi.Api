// .Converters;
using Huobi.Api.Models.RestApi.Futures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Huobi.Api.Models.RestApi.Margin
{
    /// <summary>
    /// Margin user trade page
    /// </summary>
    public class HuobiCrossMarginUserTradePage : HuobiPage
    {
        /// <summary>
        /// Trades
        /// </summary>
        public IEnumerable<HuobiCrossMarginUserTrade> Trades { get; set; } = Array.Empty<HuobiCrossMarginUserTrade>();
    }

    /// <summary>
    /// User trade info
    /// </summary>
    public class HuobiCrossMarginUserTrade : HuobiIsolatedMarginUserTrade
    {
        /// <summary>
        /// Contract type
        /// </summary>
        [JsonProperty("contract_type")]
        [JsonConverter(typeof(EnumConverter))]
        public ContractType ContractType { get; set; }
        /// <summary>
        /// Business type
        /// </summary>
        [JsonProperty("business_type")]
        [JsonConverter(typeof(EnumConverter))]
        public BusinessType BusinessType { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonProperty("pair")]
        public string Symbol { get; set; } = string.Empty;
    }
}
