import { Injectable } from '@angular/core';
import { IIngredient } from '../../interfaces/i-ingredient';
import { HttpClient } from '@angular/common/http';
import { apiPaths } from 'src/app/config/api';
import { ApiService } from 'src/app/shared/services/api.service';

@Injectable({
  providedIn: 'root'
})
export class IngredientsService extends ApiService<IIngredient>{

  constructor(
    http: HttpClient
  ) {
    super(apiPaths.ingredients, http);
  }
}
