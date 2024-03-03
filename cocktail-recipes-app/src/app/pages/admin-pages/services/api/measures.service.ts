import { Injectable } from '@angular/core';
import { IMeasure } from '../../interfaces/i-measure';
import { HttpClient } from '@angular/common/http';
import { apiPaths } from 'src/app/config/api';
import { ApiService } from 'src/app/shared/services/api.service';

@Injectable({
  providedIn: 'root'
})
export class MeasuresService extends ApiService<IMeasure>{

  constructor(
    http: HttpClient
  ) {
    super(apiPaths.measures, http);
  }
}
