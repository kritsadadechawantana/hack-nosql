using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WalletSampleApi.Models;

namespace WalletSampleApi.Dac.Contract
{
    public interface ICustomerWalletDac
    {
        CustomerWallet Get(Expression<Func<CustomerWallet, bool>> expression);
        IEnumerable<CustomerWallet> List(Expression<Func<CustomerWallet, bool>> expression);
        void Create(CustomerWallet document);
        void Update(CustomerWallet document);
        void Remove(CustomerWallet document);
    }
}