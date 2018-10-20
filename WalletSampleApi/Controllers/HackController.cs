using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WalletSampleApi.Models;
using WalletSampleApi.Dac.Contract;

namespace WalletSampleApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HackController : ControllerBase
    {
        private readonly ICustomerWalletDac customerWalletDac;
        private readonly ICoinPriceUpdateDac coinPriceUpdateDac;

        public HackController(ICustomerWalletDac customerWalletDac, ICoinPriceUpdateDac coinPriceUpdateDac)
        {
            this.customerWalletDac = customerWalletDac;
            this.coinPriceUpdateDac = coinPriceUpdateDac;
        }

        [HttpGet("{id}")]
        public ActionResult<CoinPriceUpdate> CoinPriceUpdate(string id)
        {
            return coinPriceUpdateDac.List(it => true)
            .OrderByDescending(it => it.At)
            .FirstOrDefault();
        }

        [HttpGet("{id}")]
        public ActionResult<CustomerWallet> CustomerWallet(string id)
        {
            return customerWalletDac.Get(it => it.Username == id);
        }

        [HttpPost]
        public void Post([FromBody] CoinPriceUpdate updateCoin)
        {
            var genId = Guid.NewGuid().ToString();
            updateCoin.Id = genId;
            coinPriceUpdateDac.Create(updateCoin);
        }

        public void Buy(string username, string symbol, double amount)
        {
            var currentPrice = coinPriceUpdateDac.List(it => true)
            .OrderByDescending(it => it.At)
            .FirstOrDefault();
        }

        [HttpPost]
        public void UpdateCoinPrice([FromBody] CoinPriceUpdate updateCoin)
        {
            var lastCoinPrice = coinPriceUpdateDac.List(it => true)
            .OrderByDescending(it => it.At)
            .FirstOrDefault();

            lastCoinPrice.At = updateCoin.At;
            lastCoinPrice.PriceList.ForEach(it => {
                var updateData = updateCoin.PriceList.FirstOrDefault(c => c.Symbol == it.Symbol);
                var requireUpdate = updateData != null;
                if(requireUpdate) 
                {
                    it.Buy = updateData.Buy;
                    it.Sell = updateData.Sell;
                }
            });

            coinPriceUpdateDac.Update(lastCoinPrice);
        }

        [HttpPost]
        public void InitDatabase()
        {
            var coinPriceUpdate = new List<CoinPriceUpdate>{
                new CoinPriceUpdate{
                    Id = "1",
                    At = DateTime.UtcNow,
                    PriceList = new List<CoinPrice>{
                        new CoinPrice{ Symbol = "BTC", Buy = 6565.25, Sell = 5.324},
                        new CoinPrice{ Symbol = "ETH", Buy = 203.47, Sell = 5.324},
                    }
                }
            };
            var customerWallet = new List<CustomerWallet>{
                new CustomerWallet{
                    Username = "User1",
                    Balance = 15000,
                    Coins = new List<CustomerCoin>{
                        new CustomerCoin{Symbol = "BTC", BuyingAt = DateTime.Now, Amount = 1, BuyingRate = 6565.25, USDValue = 6565.25},
                        new CustomerCoin{Symbol = "ETH", BuyingAt = DateTime.Now, Amount = 1, BuyingRate = 203.47, USDValue = 203.47}
                    }
                }
            };

            foreach (var item in coinPriceUpdate) coinPriceUpdateDac.Create(item);       
            foreach (var item in customerWallet)customerWalletDac.Create(item);
        }
    }
}
