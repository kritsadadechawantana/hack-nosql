using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Authentication;
using System.Threading.Tasks;
using WalletSampleApi.Dac.Contract;
using WalletSampleApi.Models;

namespace WalletSampleApi.Dac
{
    public class CustomerWalletDac : ICustomerWalletDac
    {
        IMongoCollection<CustomerWallet> Collection { get; set; }

        public CustomerWalletDac(DatabaseConfigurations config)
        {
            var settings = MongoClientSettings.FromUrl(new MongoUrl(config.MongoDBConnection));
            settings.SslSettings = new SslSettings()
            {
                EnabledSslProtocols = SslProtocols.Tls12
            };
            var mongoClient = new MongoClient(settings);
            var database = mongoClient.GetDatabase(config.DatabaseName);
            Collection = database.GetCollection<CustomerWallet>("customerwallet");
        }

        public CustomerWallet Get(Expression<Func<CustomerWallet, bool>> expression)
        {
            return Collection.Find(expression).FirstOrDefault();
        }

        public IEnumerable<CustomerWallet> List(Expression<Func<CustomerWallet, bool>> expression)
        {
            return Collection.Find(expression).ToList();
        }

        public void Create(CustomerWallet document)
        {
            Collection.InsertOne(document);
        }

        public void Update(CustomerWallet document)
        {
            Collection.ReplaceOne(it => it.Id == document.Id, document);
        }

        public void Remove(CustomerWallet document)
        {
            Collection.DeleteOne(it => it.Id == document.Id);
        }
    }
}