﻿namespace Huobi.Api.Models.RestApi.Futures;

/// <summary>
/// Order book entry
/// </summary>
[JsonConverter(typeof(ArrayConverter))]
public class HuobiFuturesOrderBookEntry
{
    /// <summary>
    /// The price for this entry
    /// </summary>
    [ArrayProperty(0)]
    public decimal Price { get; set; }

    /// <summary>
    /// The quantity for this entry
    /// </summary>
    [ArrayProperty(1)]
    public decimal Quantity { get; set; }

}
