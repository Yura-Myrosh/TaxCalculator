import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TaxDetailsComponent } from './tax-details.component';
import { CommonModule } from '@angular/common';
import { MatListModule } from '@angular/material/list';

describe('TaxDetailsComponent', () => {
  let component: TaxDetailsComponent;
  let fixture: ComponentFixture<TaxDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CommonModule, MatListModule, TaxDetailsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(TaxDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should display tax details correctly', () => {
    const mockTaxDetails = {
      grossAnnualSalary: 50000,
      grossMonthlySalary: 4166.67,
      netAnnualSalary: 37000,
      netMonthlySalary: 3083.33,
      annualTaxPaid: 13000,
      monthlyTaxPaid: 1083.33
    };

    component.taxDetails = mockTaxDetails;
    fixture.detectChanges();

    const listItems = fixture.nativeElement.querySelectorAll('mat-list-item');
    expect(listItems.length).toBe(6);

    expect(listItems[0].textContent).toContain(`Gross Annual Salary: £${mockTaxDetails.grossAnnualSalary.toLocaleString('en', {minimumFractionDigits: 0, maximumFractionDigits: 2})}`);
    expect(listItems[2].textContent).toContain(`Net Annual Salary: £${mockTaxDetails.netAnnualSalary.toLocaleString('en', {minimumFractionDigits: 0, maximumFractionDigits: 2})}`);
  });
});
