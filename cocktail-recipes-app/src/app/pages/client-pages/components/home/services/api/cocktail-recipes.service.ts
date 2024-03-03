import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { apiPaths } from 'src/app/config/api';
import { ApiService } from 'src/app/shared/services/api.service';
import { ICocktailRecipe } from '../../interfaces/i-cocktail-recipe';

@Injectable({
  providedIn: 'root'
})
export class CocktailRecipesService extends ApiService<ICocktailRecipe>{

  constructor(
    http: HttpClient
  ) {
    super(apiPaths.recipes, http);
  }
}