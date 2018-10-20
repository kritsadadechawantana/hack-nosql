import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  },
  {
    path: 'home',
    loadChildren: './home/home.module#HomePageModule'
  },
  
  { path: 'login', loadChildren: './login/login.module#LoginPageModule' },
  { path: 'pbuycoin', loadChildren: './pbuycoin/pbuycoin.module#PbuycoinPageModule' },
  { path: 'psalecoin', loadChildren: './psalecoin/psalecoin.module#PsalecoinPageModule' },
  { path: 'profile', loadChildren: './profile/profile.module#ProfilePageModule' }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
