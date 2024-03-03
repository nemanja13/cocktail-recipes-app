import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RecipesTableComponent } from './recipes-table.component';

describe('RecipesTableComponent', () => {
  let component: RecipesTableComponent;
  let fixture: ComponentFixture<RecipesTableComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RecipesTableComponent]
    });
    fixture = TestBed.createComponent(RecipesTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
