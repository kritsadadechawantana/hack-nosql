using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WalletSampleApi.Models;

namespace WalletSampleApi.Dac.Contract
{
    public interface ICoinPriceUpdateDac
    {
        CoinPriceUpdate Get(Expression<Func<CoinPriceUpdate, bool>> expression);
        IEnumerable<CoinPriceUpdate> List(Expression<Func<CoinPriceUpdate, bool>> expression);
        void Create(CoinPriceUpdate document);
        void Update(CoinPriceUpdate document);
        void Remove(CoinPriceUpdate document);
    }
}