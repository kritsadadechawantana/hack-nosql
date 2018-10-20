import { Component, OnInit } from '@angular/core';
import { NavController } from '@ionic/angular';

@Component({
  selector: 'app-psalecoin',
  templateUrl: './psalecoin.page.html',
  styleUrls: ['./psalecoin.page.scss'],
})
export class PsalecoinPage implements OnInit {
  constructor(public navCtrl:NavController){

  }

  ngOnInit() {
  }
  gotohome(){
    this.navCtrl.navigateForward("home")
  }
}
