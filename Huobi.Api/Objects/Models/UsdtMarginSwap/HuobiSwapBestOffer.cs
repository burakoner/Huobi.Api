﻿// .Converters;
using Huobi.Api.Models.RestApi.Spot.Trading;

namespace Huobi.Net.Objects.Models.UsdtMarginSwap;

/// <summary>
/// Best offer
/// </summary>
public class HuobiSwapBestOffer
{
    /// <summary>
    /// Business type
    /// </summary>
    [JsonConverter(typeof(EnumConverter))]
    [JsonProperty("business_type")]
    public BusinessType BusinessType { get; set; }
    /// <summary>
    /// Contract code
    /// </summary>
    [JsonProperty("contract_code")]
    public string ContractCode { get; set; } = string.Empty;
    /// <summary>
    /// Best ask
    /// </summary>
    public HuobiSpotOrderBookEntry Ask { get; set; } = null!;
    /// <summary>
    /// Best bid
    /// </summary>
    public HuobiSpotOrderBookEntry Bid { get; set; } = null!;
    /// <summary>
    /// Order id
    /// </summary>
    [JsonProperty("mrid")]
    public long Id { get; set; }
    /// <summary>
    /// Timestamp
    /// </summary>
    [JsonConverter(typeof(DateTimeConverter))]
    [JsonProperty("ts")]
    public DateTime Timestamp { get; set; }
}
