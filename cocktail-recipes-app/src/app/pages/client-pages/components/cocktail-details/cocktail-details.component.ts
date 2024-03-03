import { Component, OnDestroy, OnInit } from '@angular/core';
import { CocktailsRequestsService } from '../home/services/requests/cocktails-requests.service';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { ICocktailRecipe } from '../home/interfaces/i-cocktail-recipe';

@Component({
  selector: 'app-cocktail-details',
  templateUrl: './cocktail-details.component.html',
  styleUrls: ['./cocktail-details.component.css']
})
export class CocktailDetailsComponent implements OnInit, OnDestroy {

  constructor(
    private route: ActivatedRoute,
    private requestsService: CocktailsRequestsService
  ) { }

  public cocktail: ICocktailRecipe | null = null;
  private subscription: Subscription = new Subscription();

  ngOnInit(): void {
    this.getCocktailRecipe();
  }

  getCocktailRecipe(): void {
    let routeParams = this.route.snapshot.paramMap;
    let cocktailIdFromRoute = Number(routeParams.get('id'));
    this.subscription.add(
      this.requestsService.getCocktailRecipe(cocktailIdFromRoute).subscribe(
        (data: ICocktailRecipe) => {
          this.cocktail = data;
        }
      )
    );
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
