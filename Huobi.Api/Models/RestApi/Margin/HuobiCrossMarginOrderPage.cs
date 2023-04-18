using System;
using System.Collections.Generic;
using Huobi.Api.Models.RestApi.Futures;

namespace Huobi.Api.Models.RestApi.Margin
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
