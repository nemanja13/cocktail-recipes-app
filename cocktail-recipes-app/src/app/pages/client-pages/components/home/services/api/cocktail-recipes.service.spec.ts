import { TestBed } from '@angular/core/testing';

import { CocktailRecipesService } from './cocktail-recipes.service';

describe('CocktailRecipesService', () => {
  let service: CocktailRecipesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CocktailRecipesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
