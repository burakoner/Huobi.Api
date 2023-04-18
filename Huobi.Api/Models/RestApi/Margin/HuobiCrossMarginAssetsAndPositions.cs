using System;
using System.Collections.Generic;
using Huobi.Api.Models.RestApi.Futures;

namespace Huobi.Api.Models.RestApi.Margin
{
    /// <summary>
    /// Cross margin assets and positions info
    /// </summary>
    public class HuobiCrossMarginAssetsAndPositions : HuobiCrossMarginAccountInfo
    {
        /// <summary>
        /// Positions
        /// </summary>
        public IEnumerable<HuobiPosition> Positions { get; set; } = Array.Empty<HuobiPosition>();
    }
}
