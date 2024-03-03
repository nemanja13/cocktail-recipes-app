import { Component, OnDestroy, OnInit } from '@angular/core';
import { RecipesRequestsService } from '../../services/requests/recipes-requests.service';
import { IRecipe } from '../../interfaces/i-recipe';
import { IResponse } from 'src/app/shared/interfaces/i-response';
import { IBaseFilterRequest } from 'src/app/shared/interfaces/i-base-filter-request';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';
declare let alertify: any;

@Component({
  selector: 'app-recipes-table',
  templateUrl: './recipes-table.component.html',
  styleUrls: ['./recipes-table.component.css']
})
export class RecipesTableComponent implements OnInit, OnDestroy {

  constructor(
    private requestsService: RecipesRequestsService,
    private router: Router
  ) { }

  private subscription: Subscription = new Subscription();
  public recipes: IRecipe[] = [];
  public response: IResponse<IRecipe> | null = null;
  private pageInit: number = 1;
  private perPageInit: number = 12;
  public dataToSend: IBaseFilterRequest = {
    page: this.pageInit,
    perPage: this.perPageInit
  };

  ngOnInit(): void {
    this.getRecipes();
  }

  getRecipes(): void {
    this.subscription.add(
      this.requestsService.getRecipesWithParams(this.dataToSend).subscribe(
        (response: IResponse<IRecipe>) => {
          this.recipes = response.data;
          this.response = response;
        }
      )
    );
  }

  addEditRecipe(id?: number): void {
    if(id)
      this.router.navigateByUrl(`admin/recipe/${id}`);
    else
      this.router.navigateByUrl("admin/recipe");
  }

  deleteRecipe(id: number): void {
    this.requestsService.delete(id).subscribe({
      next: success => {
        alertify.success("You have successfully deleted the recipe");
        this.getRecipes();
      },
      error: error => {
        alertify.error("An error has occurred. Please try again later");
      }
    });
  }

  changePage(page: number): void {
    this.dataToSend.page = page;
    this.getRecipes();
  }
  
  createArray(number?: number): number[] {
    if(number == 1)
      return new Array()
    return new Array(number);
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
