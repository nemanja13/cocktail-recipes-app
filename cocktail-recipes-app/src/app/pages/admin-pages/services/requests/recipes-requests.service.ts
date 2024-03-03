import { Injectable } from '@angular/core';
import { CocktailRecipesService } from 'src/app/pages/client-pages/components/home/services/api/cocktail-recipes.service';
import { CocktailsService } from '../api/cocktails.service';
import { Observable, catchError, forkJoin, of } from 'rxjs';
import { IRecipe, IRecipeRequest } from '../../interfaces/i-recipe';
import { TypesService } from 'src/app/pages/client-pages/components/home/services/api/types.service';
import { MeasuresService } from '../api/measures.service';
import { IngredientsService } from '../api/ingredients.service';
import { IMeasure } from '../../interfaces/i-measure';
import { IIngredient } from '../../interfaces/i-ingredient';
import { IType } from 'src/app/pages/client-pages/components/home/interfaces/i-type';
import { IResponse } from 'src/app/shared/interfaces/i-response';

@Injectable({
  providedIn: 'root'
})
export class RecipesRequestsService {

  constructor(
    private apiService: CocktailsService,
    private recipesService: CocktailRecipesService,
    private typesService: TypesService,
    private measuresService: MeasuresService,
    private ingrediantsService: IngredientsService
  ) { }

  getDataFromAllRequests(id?: number): Observable<unknown> {
    let arr: Observable<unknown>[] = [
      this.getTypes().pipe(catchError(() => of([]))),
      this.getMeasures().pipe(catchError(() => of([]))),
      this.getIngredients().pipe(catchError(() => of([]))),
    ];

    if(id) {
      arr.push(this.getRecipe(id).pipe(catchError(() => of([]))));
    }

    return forkJoin(arr);
  }

  getRecipe(id: number): Observable<IRecipe> {
    return this.recipesService.get(id);
  }
  
  getRecipesWithParams(params: any): Observable<IResponse<IRecipe>> {
    return this.recipesService.getWithParams(params);
  }

  create(dataToSend: IRecipeRequest): Observable<unknown> {
    return this.apiService.create(dataToSend);
  }

  update(id: number, dataToSend: IRecipeRequest): Observable<unknown> {
    return this.apiService.update(id, dataToSend);
  }

  delete(id: number): Observable<unknown> {
    return this.apiService.delete(id);
  }

  getTypes(): Observable<IResponse<IType>> {
    return this.typesService.getAll();
  }
  
  getMeasures(): Observable<IResponse<IMeasure>> {
    return this.measuresService.getAll();
  }
  
  getIngredients(): Observable<IResponse<IIngredient>> {
    return this.ingrediantsService.getAll();
  }
}
