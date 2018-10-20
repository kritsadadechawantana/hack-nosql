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

        [HttpGet()]
        public ActionResult<CoinPriceUpdate> CoinPriceUpdate()
        {
            return coinPriceUpdateDac.List(it => true)
            .OrderByDescending(it => it.At)
            .FirstOrDefault();
        }

        [HttpGet("{username}")]
        public ActionResult<CustomerWallet> CustomerWallet(string username)
        {
            var wallet = customerWalletDac.Get(it => it.Username == username);
            var currentPrice = coinPriceUpdateDac.List(it => true)
            .OrderByDescending(it => it.At)
            .FirstOrDefault();

            wallet.Coins.ForEach(it => {
                var priceInfo = currentPrice.PriceList.FirstOrDefault(c => c.Symbol == it.Symbol); 
                it.USDValue = priceInfo.Sell * it.Amount;
            });

            return wallet;
        }

        [HttpPost]
        public ActionResult Post([FromBody] CoinPriceUpdate updateCoin)
        {
            var genId = Guid.NewGuid().ToString();
            updateCoin.Id = genId;
            coinPriceUpdateDac.Create(updateCoin);
            return Ok();
        }

        [HttpGet("{username}/{symbol}/{amount}")]
        public ActionResult Buy(string username, string symbol, double amount)
        {
            var currentPrice = coinPriceUpdateDac.List(it => true)
            .OrderByDescending(it => it.At)
            .FirstOrDefault();

            var customerWallet = customerWalletDac.Get(it => it.Username == username);

            var coinInfo = currentPrice.PriceList.FirstOrDefault(it => it.Symbol == symbol);
            var chargeBalanceAmount = coinInfo.Buy * amount;
            
            var inValidRequest = chargeBalanceAmount < customerWallet.Balance;
            if (inValidRequest) return BadRequest();

            customerWallet.Balance -= chargeBalanceAmount;
            customerWallet.Coins.Add(new CustomerCoin{
                Symbol = symbol,
                Amount = amount,
                BuyingRate = coinInfo.Buy,
                BuyingAt = DateTime.UtcNow
            });

            customerWalletDac.Update(customerWallet);

            return Ok();
        }

        [HttpGet("{username}/{id}")]
        public ActionResult Sell(string username, string id)
        {
            var currentPrice = coinPriceUpdateDac.List(it => true)
            .OrderByDescending(it => it.At)
            .FirstOrDefault();

            var customerWallet = customerWalletDac.Get(it => it.Username == username);
            var selectedCoin = customerWallet.Coins.FirstOrDefault(it => it.Id == id);
            var coinInfo = currentPrice.PriceList.FirstOrDefault(it => it.Symbol == selectedCoin.Symbol);
            var chrageBalanceAmount = coinInfo.Sell * selectedCoin.Amount;

            var inValidRequest = chrageBalanceAmount < customerWallet.Balance;
            if (inValidRequest) return BadRequest();
            
            customerWallet.Balance += chrageBalanceAmount;
            customerWallet.Coins.Remove(selectedCoin);

            customerWalletDac.Update(customerWallet);

            return Ok();
        }

        [HttpPost]
        public ActionResult UpdateCoinPrice([FromBody] CoinPriceUpdate updateCoin)
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

            return Ok();
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
                        new CustomerCoin{Id = "1", Symbol = "BTC", BuyingAt = DateTime.Now, Amount = 1, BuyingRate = 6565.25, USDValue = 6565.25},
                        new CustomerCoin{Id = "1", Symbol = "ETH", BuyingAt = DateTime.Now, Amount = 1, BuyingRate = 203.47, USDValue = 203.47}
                    }
                }
            };

            foreach (var item in coinPriceUpdate) coinPriceUpdateDac.Create(item);       
            foreach (var item in customerWallet)customerWalletDac.Create(item);
        }
    }
}
