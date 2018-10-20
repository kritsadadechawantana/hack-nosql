import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Routes, RouterModule } from '@angular/router';

import { IonicModule } from '@ionic/angular';

import { PbuycoinPage } from './pbuycoin.page';

const routes: Routes = [
  {
    path: '',
    component: PbuycoinPage
  }
];

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    IonicModule,
    RouterModule.forChild(routes)
  ],
  declarations: [PbuycoinPage]
})
export class PbuycoinPageModule {}
