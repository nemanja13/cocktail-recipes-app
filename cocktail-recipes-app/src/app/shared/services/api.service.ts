import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { config } from '../../config/global';

@Injectable({
  providedIn: 'root'
})
export class ApiService<T> {

  constructor(@Inject('apiUrl') public url : string, protected http : HttpClient) { }

  protected baseUrl = config.baseApiUrl.SERVER;

  getAll() : Observable<T[]>{
    return this.http.get<T[]>(`${ this.baseUrl + this.url}`).pipe(
      catchError(error => {
        return throwError(error.error);
      })
    );
  }

  getWithData(id: number) : Observable<T>{
    return this.http.get<T>(`${ this.baseUrl + this.url + "/"+ id }`).pipe(
      catchError(error => {
        return throwError(error.error);
      })
    );
  }

  getWithParams(params: any){
    return this.http.get<T>(`${ this.baseUrl + this.url }`, { params: params } )
    .pipe(
      catchError(error => {
        return throwError(error.error);
      })
    );
  }

  get(id: number) : Observable<T>{
    return this.http.get<T>(`${ this.baseUrl + this.url + "/"+ id }`).pipe(
      catchError(error => {
        return throwError(error.error);
      })
    );
  }

  update(id : any, obj : any){ 
    let objToSend = obj;
    let url = this.baseUrl + this.url;
    if(id){
      url += '/' + id;
    }
    return this.http.put(url, objToSend).pipe(
      catchError(error => {
        return throwError(error.error);
      })
    );
  }

  delete(id : number){
    return this.http.delete(`${ this.baseUrl + this.url + "/" + id}`).pipe(
      catchError(error => {
        return throwError(error.error);
      })
    );
  }

  create(obj : any){
    let objToSend = obj;
    return this.http.post(`${ this.baseUrl + this.url }`, objToSend).pipe(
      catchError(error => {
        return throwError(error.error);
      })
    );
  }
}
