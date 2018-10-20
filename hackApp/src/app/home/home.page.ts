import { Component } from '@angular/core';
import { NavController } from '@ionic/angular';
import { HttpClient } from '@angular/common/http';
import { CoinPriceUpdate } from '../../models/CoinPriceUpdate';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss'],
})
export class HomePage {
  priceUpdate : CoinPriceUpdate;
  constructor(public navCtrl: NavController, private http: HttpClient, public navParams: ActivatedRoute) {
    this.data();
  }
  public data(){
    this.http.get("https://hackathonwallet.azurewebsites.net/api/Hack/CoinPriceUpdate")
      .subscribe((it:CoinPriceUpdate) => 
      {
        this.priceUpdate = it;
      console.log(it)
      })
      
  }
  gotobuy(param : string) {
    this.navCtrl.navigateForward("pbuycoin/"+param);
  }
 
}
