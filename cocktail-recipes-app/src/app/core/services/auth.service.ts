import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { apiPaths } from 'src/app/config/api';
import { config } from 'src/app/config/global';
import { IAuthResponse } from '../interfaces/i-auth-response';
import { TokenService } from './token.service';
import { catchError, tap } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { IAuthRequest } from '../interfaces/i-auth-request';
declare let alertify: any;

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(
    private http: HttpClient,
    private tokenService: TokenService,
    private router: Router
  ) { }

  login(credentials: IAuthRequest){
    return this.http.post(config.baseApiUrl.SERVER + apiPaths.auth.login, credentials).pipe(
      tap(data => {
        this.tokenService.saveToken(data as IAuthResponse);

        let tokenData = this.tokenService.getTokenData();
        let actorData = JSON.parse(tokenData.ActorData);
        localStorage.setItem("actor", JSON.stringify(actorData));
      }),
      catchError(error => {
        if(error.errors){
          alertify.error(error.errors[0].ErrorMessage);
          return throwError(error.errors[0].ErrorMessage);
        }
        alertify.error('You are not authorized. Check your username and password.');
        return throwError("Not Authorized");
      })
    );
  }

  isLoggedIn(): boolean {
    return this.tokenService.hasToken();
  }

  logout(): void {
    this.tokenService.deleteToken();
    localStorage.removeItem("actor");
    this.router.navigate(["/"]).then(() => {
      window.location.reload();
    });
  }

  getActor() {
    return JSON.parse(localStorage.getItem('actor') ?? "");
  }
}
