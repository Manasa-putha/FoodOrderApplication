import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DishDetailsDialogComponent } from './dish-details-dialog.component';

describe('DishDetailsDialogComponent', () => {
  let component: DishDetailsDialogComponent;
  let fixture: ComponentFixture<DishDetailsDialogComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DishDetailsDialogComponent]
    });
    fixture = TestBed.createComponent(DishDetailsDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
