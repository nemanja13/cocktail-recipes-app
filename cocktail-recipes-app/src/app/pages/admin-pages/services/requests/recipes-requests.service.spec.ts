import { TestBed } from '@angular/core/testing';

import { RecipesRequestsService } from './recipes-requests.service';

describe('RecipesRequestsService', () => {
  let service: RecipesRequestsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RecipesRequestsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
