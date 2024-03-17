import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs'; // Import throwError
import { catchError, tap } from 'rxjs/operators';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { TaxDetails } from '../tax-details';
import { BASE_URL } from '../constants';

@Injectable({
  providedIn: 'root'
})
export class TaxCalculatorService {

  serviceUrl = "/taxcalculator/v1";
  constructor(private http: HttpClient) { }

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };

  getTaxDetails(salary: Number): Observable<TaxDetails> {
    const methodUrl = `${BASE_URL}${this.serviceUrl}/$calculateTaxes/${salary}`;
    return this.http.get<TaxDetails>(methodUrl, this.httpOptions)
      .pipe(
        tap(_ => this.log('fetched tax details')),
        catchError(this.handleError<TaxDetails>('getTaxDetails'))
      );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      this.log(`${operation} failed: ${error.message}`);
      return throwError(() => error);
    };
  }
  
  private log(message: string) {
    console.log(`TaxCalculatorService: ${message}`);
  }
}