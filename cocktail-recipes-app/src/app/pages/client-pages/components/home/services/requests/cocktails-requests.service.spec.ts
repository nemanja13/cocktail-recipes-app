import { TestBed } from '@angular/core/testing';

import { CocktailsRequestsService } from './cocktails-requests.service';

describe('CocktailsRequestsService', () => {
  let service: CocktailsRequestsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CocktailsRequestsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
