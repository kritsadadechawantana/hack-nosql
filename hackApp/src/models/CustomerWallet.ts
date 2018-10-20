export class CustomerWallet
{
    Username : string;
    Balance : number;
    Coins : CustomerCoin[];
}

export class CustomerCoin
{
    Id : string;
    Symbol : string;
    BuyingRate : number;
    BuyingAt : Date;
    USDValue : number;
    Amount : number;
}