import { Component, OnInit } from '@angular/core';
import { NavController } from '@ionic/angular';
import { HttpClient } from '@angular/common/http';
import { CustomerWallet } from '../../models/CustomerWallet';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.page.html',
  styleUrls: ['./profile.page.scss'],
})
export class ProfilePage implements OnInit {

  Wallet : CustomerWallet;
  constructor(public navCtrl: NavController, private http: HttpClient) {
    this.data();
  }
  public data(){
    this.http.get("https://hackathonwallet.azurewebsites.net/api/Hack/CustomerWallet/User1")
      .subscribe((it:CustomerWallet) => 
      {
        this.Wallet = it;
      console.log(it)
      })
    }
  gotosale(){
    this.navCtrl.navigateForward("psalecoin") 
  }

  ngOnInit() {
  }

}
