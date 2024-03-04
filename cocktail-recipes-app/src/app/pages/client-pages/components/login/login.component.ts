import { Component, OnDestroy, OnInit } from '@angular/core';
import { Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { IAuthRequest } from 'src/app/core/interfaces/i-auth-request';
import { AuthService } from 'src/app/core/services/auth.service';
import { FormBuilderTypeSafe, FormGroupTypeSafe } from 'src/app/shared/helper/reactive-forms-helper';
declare let alertify: any;

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit, OnDestroy {

  constructor(
    private formBuilder: FormBuilderTypeSafe,
    private authService: AuthService,
    private router: Router
  ) { }

  private subscription: Subscription = new Subscription();
  public loginForm: FormGroupTypeSafe<IAuthRequest> = this.formBuilder.group<IAuthRequest>({
    username: this.formBuilder.control("", [Validators.required]),
    password: this.formBuilder.control("", [Validators.required])
  });

  ngOnInit(): void {
  }

  submitLogin(): void {
    if(this.loginForm.invalid){
      if(this.loginForm.getSafe(x => x.username).hasError('required')){
        alertify.error("Username is required");
        return;
      }
      if(this.loginForm.getSafe(x => x.password).hasError('required')){
        alertify.error("Password is required");
        return;
      }
    }else{
      let formValue: IAuthRequest = this.loginForm.value as IAuthRequest;

      this.subscription.add(
        this.authService.login(formValue).subscribe({
          next: success => {
            alertify.success("You have successfully logged in");
            this.loginForm.reset();
            this.router.navigateByUrl('/admin')
          },
          error: error => {
            if(error.errors){
              alertify.error(error.errors[0].ErrorMessage);
            }
          }
        })
      );
    }
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

}
