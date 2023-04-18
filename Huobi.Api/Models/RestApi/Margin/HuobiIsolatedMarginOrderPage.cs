using System;
using System.Collections.Generic;
using Huobi.Api.Models.RestApi.Futures;

namespace Huobi.Api.Models.RestApi.Margin
{
    /// <summary>
    /// Order page
    /// </summary>
    public class HuobiIsolatedMarginOrderPage : HuobiPage
    {
        /// <summary>
        /// Orders
        /// </summary>
        public IEnumerable<HuobiIsolatedMarginOrder> Orders { get; set; } = Array.Empty<HuobiIsolatedMarginOrder>();
    }
}
