import { Injectable } from '@angular/core';
import { IType } from '../../interfaces/i-type';
import { HttpClient } from '@angular/common/http';
import { apiPaths } from 'src/app/config/api';
import { ApiService } from 'src/app/shared/services/api.service';

@Injectable({
  providedIn: 'root'
})
export class TypesService extends ApiService<IType>{

  constructor(
    http: HttpClient
  ) {
    super(apiPaths.types, http);
  }
}