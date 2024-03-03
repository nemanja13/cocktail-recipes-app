import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { IAuthRequest } from 'src/app/core/interfaces/i-auth-request';
import { AuthService } from 'src/app/core/services/auth.service';
declare let alertify: any;

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) { }

  public loginForm: FormGroup = this.formBuilder.group({
    username: this.formBuilder.control("", [Validators.required]),
    password: this.formBuilder.control("", [Validators.required])
  });

  ngOnInit(): void {
  }

  submitLogin(){
    if(this.loginForm.invalid){
      if((this.loginForm.get('username') as FormControl).hasError('required')){
        alertify.error("Username is required");
        return;
      }
      if((this.loginForm.get('password') as FormControl).hasError('required')){
        alertify.error("Password is required");
        return;
      }
    }else{
      let formValue: IAuthRequest = this.loginForm.value as IAuthRequest;

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
    }
  }

}
