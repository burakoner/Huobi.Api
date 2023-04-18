using ApiSharp.Extensions;
using ApiSharp.Logging;
using ApiSharp.Models;
using ApiSharp.Stream;
using Huobi.Api.Models.StreamApi.Futures;
using Huobi.Api.Models.StreamApi.Spot;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Globalization;

namespace ConsoleApp1;

internal class Program
{
    static async Task Main(string[] args)
    {
        var api = new Huobi.Api.HuobiRestApiClient(new Huobi.Api.HuobiRestApiClientOptions
        {
            SignPublicRequests = true,
            RawResponse = true,
        });

        //var system_01 = await api.System.GetSystemStatusAsync();
        //var spot_01 = await api.Spot.MasterData.GetMarketStatusAsync();
        //var spot_02 = await api.Spot.MasterData.GetSymbolsAsync();
        //var spot_03 = await api.Spot.MasterData.GetServerTimeAsync();
        //var spot_04 = await api.Spot.MasterData.GetCurrenciesAsync();
        //var spot_05 = await api.Spot.MasterData.GetCurrencySettingsAsync();
        //var spot_06 = await api.Spot.MasterData.GetCurrencySettingsAsync(DateTime.Now);
        //var spot_07 = await api.Spot.MasterData.GetSymbolSettingsAsync();
        //var spot_08 = await api.Spot.MasterData.GetMarketSymbolSettingsAsync();
        //var spot_09 = await api.Spot.MasterData.GetBlockchainsAsync();
        //var spot_10 = await api.Spot.MasterData.GetCurrencyChainsAsync();
        //var spot_201 = await api.Spot.MarketData.GetKlinesAsync("btcusdt", Huobi.Api.Enums.KlineInterval.OneHour, 100);
        //var spot_202 = await api.Spot.MarketData.GetTickerAsync("btcusdt");
        //var spot_202 = await api.Spot.MarketData.GetTickersAsync();
        //var spot_202 = await api.Spot.MarketData.GetOrderBookAsync("btcusdt");
        //var spot_203 = await api.Spot.MarketData.GetLastTradeAsync("btcusdt");
        //var spot_204 = await api.Spot.MarketData.GetTradesAsync("btcusdt", 100);
        //var spot_205 = await api.Spot.Public.GetTradesAsync("btcusdt", 100);
        //var spot_206 = await api.Spot.Public.GetMarketSummaryAsync("btcusdt");
        //var users_01 = await api.Users.GetUserIdAsync();
        //var users_02 = await api.Users.GetSubUsersAsync();
        //var wallet_01 = await api.Wallet.GetDepositAddressAsync("usdt");
        //var wallet_01 = await api.Wallet.GetAccountsAsync();
        //var trading_02 = await api.Spot.Trading.GetOpenOrdersAsync();
        /** /
        var trading_01 = await api.Spot.Trading.PlaceOrderAsync(new HuobiOrderRequest
        {
            AccountId = 6836888,
            Symbol = "xrpusdt",
            Side = Huobi.Api.Enums.SpotOrderSide.Buy,
            Type = Huobi.Api.Enums.SpotOrderType.Limit,
            Quantity = 100m,
            Price = 0.37510m,
            Source = Huobi.Api.Enums.OrderSource.Spot,
        });
        /**/
        /*
        var trading_01 = await api.Spot.Trading.PlaceOrderAsync(new HuobiOrderRequest
        {
            AccountId = 6836888,
            Symbol = "adausdt",
            Side = Huobi.Api.Enums.SpotOrderSide.Buy,
            Type = Huobi.Api.Enums.SpotOrderType.Limit,
            Quantity = 100m,
            Price = 0.365200m,
            Source = Huobi.Api.Enums.OrderSource.Spot,
        });
        */

        //var futures_01 = await api.Futures.GetOrderBookAsync("btc_cq");
        //var futures_02 = await api.Futures.GetBestOrdersAsync("btc_cq");
        //var futures_03 = await api.Futures.GetKlinesAsync("btc_cq", Huobi.Api.Enums.KlineInterval.OneHour, 100);
        //var futures_04 = await api.Futures.GetMarkPriceKlinesAsync("btc_cq", Huobi.Api.Enums.KlineInterval.OneHour, 100);









        var abc = 0;


        /*
        var ws = new Huobi.Api.HuobiStreamClient(new Huobi.Api.HuobiStreamClientOptions
        {
            RawResponse = true,
            ApiCredentials = new ApiSharp.Authentication.ApiCredentials("ebd6ea04-b6053d4a-edrfhh5h53-9e16d", "dd602ecd-b0e3339a-a05e1aaf-8a342")
        });
        /*
        var ws01 = await ws.Spot.GetKlinesAsync("btcusdt", Huobi.Api.Enums.KlineInterval.OneHour);
        var ws02 = await ws.Spot.SubscribeToKlineUpdatesAsync("btcusdt", Huobi.Api.Enums.KlineInterval.OneHour, data =>
        {
            Console.WriteLine($"btcusdt {data.Data.OpenTime}, {data.Data.OpenPrice}, {data.Data.HighPrice}, {data.Data.LowPrice}, {data.Data.ClosePrice}, {data.Data.Volume}, {data.Data.QuoteVolume}, {data.Data.TradeCount}");
        });
        var ws03 = await ws.Spot.SubscribeToTickerUpdatesAsync("btcusdt", data =>
        {
            Console.WriteLine($"{data.Data.Symbol} {data.Data.OpenPrice}, {data.Data.HighPrice}, {data.Data.LowPrice}, {data.Data.ClosePrice}, {data.Data.Volume}, {data.Data.QuoteVolume}, {data.Data.TradeCount}, {data.Data.Version}");
        });
        var ws04 = ws.Spot.SubscribeToTickerUpdatesAsync(data =>
        {
            foreach (var item in data.Data.Ticks)
            {
                Console.WriteLine($"{item.Symbol} {item.OpenPrice}, {item.HighPrice}, {item.LowPrice}, {item.ClosePrice}, {item.Volume}, {item.QuoteVolume}, {item.TradeCount}, {item.Version}");
            }
        });
        var ws05 = await ws.Spot.GetMergedOrderBookAsync("btcusdt",0);
        var ws06 = await ws.Spot.GetOrderBookAsync("btcusdt", 20);
        */
        //var ws07 = ws.Spot.SubscribeToPartialOrderBookUpdates1SecondAsync
        //var ws01 = ws.Spot.SubscribeToPartialOrderBookUpdates100MilisecondAsync
        //var ws01 = ws.Spot.SubscribeToOrderBookChangeUpdatesAsync
        //var ws01 = ws.Spot.GetTradeHistoryAsync
        //var ws01 = ws.Spot.SubscribeToTradeUpdatesAsync
        //var ws01 = ws.Spot.GetSymbolDetailsAsync
        //var ws01 = ws.Spot.SubscribeToSymbolDetailUpdatesAsync
        //var ws01 = ws.Spot.SubscribeToBestOfferUpdatesAsync
        //var ws01 = ws.Spot.SubscribeToOrderUpdatesAsync
        //var ws01 = ws.Spot.SubscribeToAccountUpdatesAsync
        //var ws01 = ws.Spot.SubscribeToOrderDetailsUpdatesAsync

        Console.ReadLine();

        var y = 0;
        var z = 0;
    }
}