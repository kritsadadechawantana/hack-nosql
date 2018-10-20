export class CoinPriceUpdate{
    Id : string;
    At : Date;
    PriceList : CoinPrice[] ;
}

export class CoinPrice
{
    Symbol : string ;
    Buy : number;
    Sell : number;
}