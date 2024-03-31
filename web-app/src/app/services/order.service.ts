import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  constructor(private readonly client: HttpClient) { }

  public getOrdersForCompany(companyId: number): Observable<any> {
    return this.client.get<any>(`/api/order/${companyId}`)
      .pipe(
        catchError((error: HttpErrorResponse) => {        
          console.error('An error occurred:', error.error.message || error.statusText);
          return throwError(()=> new Error('Something went wrong. Please try again later.'));
        })
      );
  }
}
