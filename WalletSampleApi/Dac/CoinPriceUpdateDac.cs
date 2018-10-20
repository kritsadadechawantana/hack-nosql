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
    public class CoinPriceUpdateDac : ICoinPriceUpdateDac
    {
        IMongoCollection<CoinPriceUpdate> Collection { get; set; }

        public CoinPriceUpdateDac(DatabaseConfigurations config)
        {
            var settings = MongoClientSettings.FromUrl(new MongoUrl(config.MongoDBConnection));
            settings.SslSettings = new SslSettings()
            {
                EnabledSslProtocols = SslProtocols.Tls12
            };
            var mongoClient = new MongoClient(settings);
            var database = mongoClient.GetDatabase(config.DatabaseName);
            Collection = database.GetCollection<CoinPriceUpdate>("coinpriceupdate");
        }

        public CoinPriceUpdate Get(Expression<Func<CoinPriceUpdate, bool>> expression)
        {
            return Collection.Find(expression).FirstOrDefault();
        }

        public IEnumerable<CoinPriceUpdate> List(Expression<Func<CoinPriceUpdate, bool>> expression)
        {
            return Collection.Find(expression).ToList();
        }

        public void Create(CoinPriceUpdate document)
        {
            Collection.InsertOne(document);
        }

        public void Update(CoinPriceUpdate document)
        {
            Collection.ReplaceOne(it => it.Id == document.Id, document);
        }

        public void Remove(CoinPriceUpdate document)
        {
            Collection.DeleteOne(it => it.Id == document.Id);
        }
    }
}