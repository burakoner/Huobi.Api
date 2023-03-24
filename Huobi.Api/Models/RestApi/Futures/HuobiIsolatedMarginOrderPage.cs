using System;
using System.Collections.Generic;

namespace Huobi.Api.Models.RestApi.Futures
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
