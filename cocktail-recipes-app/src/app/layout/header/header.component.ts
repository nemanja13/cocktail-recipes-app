import { Component, OnDestroy, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AuthService } from 'src/app/core/services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit, OnDestroy {

  constructor(
    private authService: AuthService,
    private router: Router
  ) { }

  private subscription: Subscription = new Subscription();
  public isLoggedIn: boolean = false;

  ngOnInit(): void {
    this.isLoggedIn = this.authService.isLoggedIn();
    this.trackRouteChange();
  }

  trackRouteChange(): void {
    this.subscription.add(
      this.router.events.subscribe({
        next: event => {
          if (event instanceof NavigationEnd) {
            this.isLoggedIn = this.authService.isLoggedIn();
          }
        }
      })
    );
  }

  logout(): void {
    this.authService.logout();
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

}
