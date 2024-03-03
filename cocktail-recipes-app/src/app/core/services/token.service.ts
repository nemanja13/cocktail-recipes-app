import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { IAuthResponse } from '../interfaces/i-auth-response';

@Injectable({
  providedIn: 'root'
})
export class TokenService {

  constructor() { }

  private jwtService = new JwtHelperService();

  saveToken(data: IAuthResponse){
    localStorage.setItem("token", JSON.stringify(data));
  }

  hasToken(): boolean {
    if(this.jwtService.isTokenExpired(this.getToken())){
      this.deleteToken();
    }
    return localStorage.getItem("token") != null && localStorage.getItem("token") != "";
  }

  getToken() {
    let token = null;
    try {
      token = JSON.parse(localStorage.getItem("token") ?? "");
      let data = token as IAuthResponse;
      return data.token;
    }
    catch(e){
      return token;
    }
  }

  deleteToken(): void {
    localStorage.removeItem("token");
  }

  getTokenData() {
    let token = null;
    try {
      token = JSON.parse(localStorage.getItem("token") ?? "");
      let data = token as IAuthResponse;
      return this.jwtService.decodeToken(data.token);

    } catch (e) {
        return token;
    }
  }
}
