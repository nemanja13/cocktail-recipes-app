import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CocktailRecipesService } from '../api/cocktail-recipes.service';
import { ICocktailRecipe } from '../../interfaces/i-cocktail-recipe';
import { TypesService } from '../api/types.service';
import { IType } from '../../interfaces/i-type';
import { IResponse } from 'src/app/shared/interfaces/i-response';

@Injectable({
  providedIn: 'root'
})
export class CocktailsRequestsService {

  constructor(
    private apiService: CocktailRecipesService,
    private typesService: TypesService
  ) { }

  getCocktailRecipes(): Observable<IResponse<ICocktailRecipe>> {
    return this.apiService.getAll();
  }

  getCocktailRecipesWithParams(params: any): Observable<IResponse<ICocktailRecipe>> {
    return this.apiService.getWithParams(params);
  }

  getCocktailRecipe(id: number): Observable<ICocktailRecipe> {
    return this.apiService.get(id);
  }

  getTypes(): Observable<IResponse<IType>> {
    return this.typesService.getAll();
  }
}
