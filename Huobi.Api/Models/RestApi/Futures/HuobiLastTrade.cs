// .Converters;
using Newtonsoft.Json;
using System;

namespace Huobi.Api.Models.RestApi.Futures
{
    internal class HuobiLastTradeWrapper
    {
        public HuobiLastTrade[] Data { get; set; } = Array.Empty<HuobiLastTrade>();
    }

    internal class HuobiTradeWrapper
    {
        public HuobiTrade[] Data { get; set; } = Array.Empty<HuobiTrade>();
    }

    /// <summary>
    /// Last trade data
    /// </summary>
    public class HuobiTrade
    {
        /// <summary>
        /// Amount of contracts
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Quantity
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonProperty("ts")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Direction
        /// </summary>
        public SpotOrderSide Direction { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        [JsonProperty("trade_turnover")]
        public decimal QuoteQuantity { get; set; }
    }

    /// <summary>
    /// Last trade info
    /// </summary>
    public class HuobiLastTrade : HuobiTrade
    {
        /// <summary>
        /// Contract code
        /// </summary>
        [JsonProperty("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// Business type
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        [JsonProperty("business_type")]
        public BusinessType BusinessType { get; set; }
    }

    internal class HuobiFuturesLastTradeWrapper
    {
        public HuobiFuturesLastTrade[] Data { get; set; } = Array.Empty<HuobiFuturesLastTrade>();
    }
    internal class HuobiFuturesTradeWrapper
    {
        public HuobiFuturesTrade[] Data { get; set; } = Array.Empty<HuobiFuturesTrade>();
    }

    /// <summary>
    /// Last trade info
    /// </summary>
    public class HuobiFuturesTrade
    {
        /// <summary>
        /// Amount of contracts
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Quantity
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonProperty("ts")]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Direction
        /// </summary>
        public SpotOrderSide Direction { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        [JsonProperty("price")]
        public decimal Price { get; set; }
    }

    /// <summary>
    /// Last trade info
    /// </summary>
    public class HuobiFuturesLastTrade: HuobiFuturesTrade
    {
        /// <summary>
        /// Contract code
        /// </summary>
        [JsonProperty("symbol")]
        public string Symbol { get; set; } = string.Empty;
    }
}
