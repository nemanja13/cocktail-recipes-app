import { Component, OnDestroy, OnInit } from '@angular/core';
import { CocktailsRequestsService } from './services/requests/cocktails-requests.service';
import { Subscription, debounceTime } from 'rxjs';
import { Router } from '@angular/router';
import { ICocktailRecipe, ICocktailRecipeDropdownData, ICocktailRecipeFilterForm, ICocktailRecipeFilterRequest } from './interfaces/i-cocktail-recipe';
import { IResponse } from 'src/app/shared/interfaces/i-response';
import { ORDER_BY_OPTIONS } from 'src/app/config/settings/order-by-options';
import { FormBuilderTypeSafe, FormGroupTypeSafe } from 'src/app/shared/helper/reactive-forms-helper';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit, OnDestroy {

  constructor(
    private requestsService: CocktailsRequestsService,
    private formBuilder: FormBuilderTypeSafe,
    private router: Router
  ) { }
  
  public filterForm: FormGroupTypeSafe<ICocktailRecipeFilterForm> = this.formBuilder.group<ICocktailRecipeFilterForm>({
    keyword: this.formBuilder.control(null),
    typeId: this.formBuilder.control(null),
    orderBy: this.formBuilder.control(null)
  });

  public cocktails: ICocktailRecipe[] = [];
  public response: IResponse<ICocktailRecipe> | null = null;
  public subscription: Subscription = new Subscription();
  public dropdownData: ICocktailRecipeDropdownData = {
    Types: [],
    OrderByOptions: ORDER_BY_OPTIONS
  };
  public dataToSend: ICocktailRecipeFilterRequest = {
    page: 1,
    perPage: 12,
    keyword: null,
    typeId: null,
    orderBy: null
  };

  ngOnInit(): void {
    this.getCocktailRecipes();
    this.getTypes();
    this.trackFilterChange();
  }

  getCocktailRecipes(): void {
    this.dataToSend.keyword = this.filterForm.getSafe(x => x.keyword).value;
    this.dataToSend.typeId = +this.filterForm.getSafe(x => x.typeId).value;
    this.dataToSend.orderBy = +this.filterForm.getSafe(x => x.orderBy).value;
    this.subscription.add(
      this.requestsService.getCocktailRecipesWithParams(this.dataToSend).subscribe(
        (response: IResponse<ICocktailRecipe>) => {
          this.cocktails = response.data;
          this.response = response;
        }
      )
    );
  }

  getTypes(): void {
    this.subscription.add(
      this.requestsService.getTypes().subscribe({
        next: response => {
          this.dropdownData.Types = response.data;
        }
      })
    );
  }

  showDetails(id: number): void {
    this.router.navigateByUrl(`/cocktails/${id}`);
  }
  
  makeArray(num: number = 0): number[] {
    return new Array(num);
  }

  changePage(page: number): void {
    this.dataToSend.page = page;
    this.getCocktailRecipes();
  }

  perPage(perPage: number): void {
    this.cocktails = [];
    this.dataToSend.perPage = perPage;
    this.getCocktailRecipes();
  }

  trackFilterChange(): void {
    this.subscription.add(
      this.filterForm.valueChanges.pipe(debounceTime(500)).subscribe({
        next: data => {
          console.log(data)
          this.getCocktailRecipes();
        }
      })
    );
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

}
