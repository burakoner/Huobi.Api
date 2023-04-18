using System;
using System.Collections.Generic;
using System.Text;
using Huobi.Api.Models.RestApi.Futures;

namespace Huobi.Api.Models.RestApi.Margin
{
    /// <summary>
    /// Isolated margin assets and positions info
    /// </summary>
    public class HuobiIsolatedMarginAssetsAndPositions : HuobiIsolatedMarginAccountInfo
    {
        /// <summary>
        /// Positions
        /// </summary>
        public IEnumerable<HuobiPosition> Positions { get; set; } = Array.Empty<HuobiPosition>();
    }
}
