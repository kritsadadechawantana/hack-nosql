import { Component, OnInit } from '@angular/core';
import { NavController } from '@ionic/angular';
import { ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-pbuycoin',
  templateUrl: './pbuycoin.page.html',
  styleUrls: ['./pbuycoin.page.scss'],
})
export class PbuycoinPage implements OnInit {

  sym: any;
  amount : number;

  constructor(public navCtrl: NavController, public acti: ActivatedRoute, public http: HttpClient) {
    this.sym = acti.snapshot.paramMap.get("param")
  }

  ngOnInit() {
  }

  gotohome() {
    this.http.get("https://hackathonwallet.azurewebsites.net/api/Hack/Buy/User1/"+this.sym+"/"+this.amount)
    .subscribe(it => 
    this.navCtrl.navigateForward("home"))
  }
}
