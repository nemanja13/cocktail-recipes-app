import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { TokenService } from '../core/services/token.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(
    private router: Router, 
    private jwtHelper: JwtHelperService,
    private tokenService: TokenService
  ){}

  canActivate(): boolean {
    if(this.tokenService.hasToken() && !this.jwtHelper.isTokenExpired(this.tokenService.getToken())){
      return true;
    }
    if(this.jwtHelper.isTokenExpired(this.tokenService.getToken())){
      this.tokenService.deleteToken();
    }
    this.router.navigate(["/login"]);
    return false;
  }
  
}