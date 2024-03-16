import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaxDetailsComponent } from './tax-details.component';

describe('TaxDetailsComponent', () => {
  let component: TaxDetailsComponent;
  let fixture: ComponentFixture<TaxDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TaxDetailsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TaxDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
