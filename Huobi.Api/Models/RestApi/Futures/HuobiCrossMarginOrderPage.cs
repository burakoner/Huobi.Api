using System;
using System.Collections.Generic;

namespace Huobi.Api.Models.RestApi.Futures
{
    /// <summary>
    /// Order page
    /// </summary>
    public class HuobiCrossMarginOrderPage : HuobiPage
    {
        /// <summary>
        /// Orders
        /// </summary>
        public IEnumerable<HuobiCrossMarginOrder> Orders { get; set; } = Array.Empty<HuobiCrossMarginOrder>();
    }
}
