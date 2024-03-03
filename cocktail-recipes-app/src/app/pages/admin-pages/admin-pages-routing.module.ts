import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RecipesTableComponent } from './components/recipes-table/recipes-table.component';
import { RecipesFormComponent } from './components/recipes-form/recipes-form.component';

const routes: Routes = [
  {
    path: "",
    component: RecipesTableComponent
  },
  {
    path: "recipe/:id",
    component: RecipesFormComponent
  },
  {
    path: "recipe",
    component: RecipesFormComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminPagesRoutingModule { }
