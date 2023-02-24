﻿using System;
// .Converters;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models
{
    /// <summary>
    /// Symbol data
    /// </summary>
    public class HuobiSymbolData
    {
        /// <summary>
        /// The highest price
        /// </summary>
        [JsonProperty("high")]
        public decimal? HighPrice { get; set; }
        /// <summary>
        /// The lowest price
        /// </summary>
        [JsonProperty("low")]
        public decimal? LowPrice { get; set; }
        /// <summary>
        /// The price at the opening
        /// </summary>
        [JsonProperty("open")]
        public decimal? OpenPrice { get; set; }
        /// <summary>
        /// The last price
        /// </summary>
        [JsonProperty("close")]
        public decimal? ClosePrice { get; set; }
        /// <summary>
        /// The volume in base asset
        /// </summary>
        [JsonProperty("amount")]
        public decimal? Volume { get; set; }
        /// <summary>
        /// The volume in quote asset (quantity * price)
        /// </summary>
        [JsonProperty("vol")]
        public decimal? QuoteVolume { get; set; }
        /// <summary>
        /// The number of trades
        /// </summary>
        [JsonProperty("count")]
        public int? TradeCount { get; set; }
        /// <summary>
        /// Version
        /// </summary>
        public long? Version { get; set; }
    }

    /// <summary>
    /// Ticker data
    /// </summary>
    public class HuobiSymbolTicker : HuobiSymbolData
    {
        /// <summary>
        /// The symbol
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
    }

    /// <summary>
    /// Symbol details
    /// </summary>
    public class HuobiSymbolDetails : HuobiSymbolData
    {
        /// <summary>
        /// The id of the details
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Timestamp of the data
        /// </summary>
        public DateTime Timestamp { get; set; }
    }

}
