import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ClientPagesRoutingModule } from './client-pages-routing.module';
import { CocktailDetailsComponent } from './components/cocktail-details/cocktail-details.component';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { SharedModule } from 'src/app/shared/shared.module';


@NgModule({
  declarations: [
    CocktailDetailsComponent,
    HomeComponent,
    LoginComponent
  ],
  imports: [
    CommonModule,
    ClientPagesRoutingModule,
    SharedModule
  ]
})
export class ClientPagesModule { }
