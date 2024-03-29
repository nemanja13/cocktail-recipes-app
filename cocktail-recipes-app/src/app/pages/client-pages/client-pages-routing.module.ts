import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CocktailDetailsComponent } from './components/cocktail-details/cocktail-details.component';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';

const routes: Routes = [
  {
    path: "",
    component: HomeComponent
  },
  {
    path: "cocktails/:id",
    component: CocktailDetailsComponent
  },
  {
    path: "login",
    component: LoginComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ClientPagesRoutingModule { }
