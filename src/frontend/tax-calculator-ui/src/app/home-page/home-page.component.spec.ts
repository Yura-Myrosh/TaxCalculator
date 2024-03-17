import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HomePageComponent } from './home-page.component';
import { TaxCalculatorService } from '../services/tax-calculator.service';
import { TaxDetails } from '../tax-details';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { of } from 'rxjs';

describe('HomePageComponent', () => {
  let component: HomePageComponent;
  let taxCalculatorServiceSpy: jasmine.SpyObj<TaxCalculatorService>;
  let fixture: ComponentFixture<HomePageComponent>;

  beforeEach(async () => {
    taxCalculatorServiceSpy = jasmine.createSpyObj('TaxCalculatorService', ['getTaxDetails']);

    await TestBed.configureTestingModule({
      imports: [HomePageComponent, BrowserAnimationsModule],
      providers: [
        { provide: TaxCalculatorService, useValue: taxCalculatorServiceSpy}
      ]
    }).compileComponents();
    fixture = TestBed.createComponent(HomePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  describe('calculateTax', () => {
    it('should update taxDetails correctly when getTaxDetails is called', (done) => {
      const mockSalary = 70000;
      const mockTaxDetails: TaxDetails = {
        grossAnnualSalary: 70000,
        grossMonthlySalary: 5833.33,
        netAnnualSalary: 52000,
        netMonthlySalary: 4333.33,
        annualTaxPaid: 18000,
        monthlyTaxPaid: 1500
      };
      taxCalculatorServiceSpy.getTaxDetails.and.returnValue(of(mockTaxDetails));

      component.salary = mockSalary;
      component.calculateTax();

      fixture.detectChanges();

      fixture.whenStable().then(() => {
        expect(component.taxDetails).toEqual(jasmine.objectContaining({
          grossAnnualSalary: 70000,
          grossMonthlySalary: jasmine.any(Number),
          netAnnualSalary: 52000,
          netMonthlySalary: jasmine.any(Number),
          annualTaxPaid: 18000,
          monthlyTaxPaid: jasmine.any(Number)
        }));
        done();
      });
    });
  });
});