import { TestBed } from '@angular/core/testing';
import { HttpClient } from '@angular/common/http';
import { TaxCalculatorService } from './tax-calculator.service';
import { TaxDetails } from '../tax-details';
import { of, throwError } from 'rxjs';

describe('TaxCalculatorServiceService', () => {
  let service: TaxCalculatorService;
  let httpClientSpy: jasmine.SpyObj<HttpClient>;

  beforeEach(() => {
    httpClientSpy = jasmine.createSpyObj('HttpClient', ['get', 'post']);
    TestBed.configureTestingModule({
      providers: [
        { provide: HttpClient, useValue: httpClientSpy }
      ]
    });
    service = TestBed.inject(TaxCalculatorService);
    
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should return expected tax details (HttpClient called once)', (done: DoneFn) => {
    const expectedTaxDetails: TaxDetails = {
      grossAnnualSalary: 50000,
      grossMonthlySalary: 4166.67,
      netAnnualSalary: 37000,
      netMonthlySalary: 3083.33,
      annualTaxPaid: 13000,
      monthlyTaxPaid: 1083.33
    };

    httpClientSpy.get.and.returnValue(of(expectedTaxDetails));

    service.getTaxDetails(50000).subscribe({
      next: taxDetails => {
        expect(taxDetails).toEqual(expectedTaxDetails, 'expected tax details');
        done();
      },
      error: done.fail
    });
    expect(httpClientSpy.get.calls.count()).toBe(1, 'one call');
  });

  it('should handle error correctly when server returns a 404', (done: DoneFn) => {
    const errorResponse = new ErrorEvent('API error', {
      message: 'Resource not found',
    });

    httpClientSpy.get.and.returnValue(throwError(() => errorResponse));

    service.getTaxDetails(50000).subscribe({
      next: taxDetails => {
        fail('expected an error, not tax details');
      },
      error: error => {
        expect(error).toBe(errorResponse);
        done();
      }
    });

    expect(httpClientSpy.get.calls.count()).toBe(1, 'one call');
  });
});
