import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AdminPagesRoutingModule } from './admin-pages-routing.module';
import { RecipesTableComponent } from './components/recipes-table/recipes-table.component';
import { RecipesFormComponent } from './components/recipes-form/recipes-form.component';
import { SharedModule } from 'src/app/shared/shared.module';


@NgModule({
  declarations: [
    RecipesTableComponent,
    RecipesFormComponent
  ],
  imports: [
    CommonModule,
    AdminPagesRoutingModule,
    SharedModule
  ]
})
export class AdminPagesModule { }
