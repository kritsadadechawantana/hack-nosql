import { Component, OnInit } from '@angular/core';
import { NavController } from '@ionic/angular';

@Component({
  selector: 'app-pbuycoin',
  templateUrl: './pbuycoin.page.html',
  styleUrls: ['./pbuycoin.page.scss'],
})
export class PbuycoinPage implements OnInit {

  constructor(public navCtrl:NavController){

  }

  ngOnInit() {
  }
  gotohome(){
    this.navCtrl.navigateForward("home")
  }
}
