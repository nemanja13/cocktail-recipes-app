import { Injectable } from '@angular/core';
import { IRecipe } from '../../interfaces/i-recipe';
import { HttpClient } from '@angular/common/http';
import { apiPaths } from 'src/app/config/api';
import { ApiService } from 'src/app/shared/services/api.service';

@Injectable({
  providedIn: 'root'
})
export class CocktailsService extends ApiService<IRecipe>{

  constructor(
    http: HttpClient
  ) {
    super(apiPaths.cocktails, http);
  }
}
