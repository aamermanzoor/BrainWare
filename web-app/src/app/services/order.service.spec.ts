import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { OrderService } from './order.service';
import { HttpErrorResponse } from '@angular/common/http';

describe('OrderService', () => {
  let service: OrderService;
  let httpTestingController: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [OrderService]
    });
    service = TestBed.inject(OrderService);
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpTestingController.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should return orders for company', () => {
    const companyId = 1;
    const mockResponse = [{ orderId: 1, description: 'Order 1', orderTotal: 100 }];

    service.getOrdersForCompany(companyId).subscribe(orders => {
      expect(orders).toEqual(mockResponse);
    });

    const req = httpTestingController.expectOne(`/api/order/${companyId}`);
    expect(req.request.method).toBe('GET');
    req.flush(mockResponse);
  });

  it('should handle errors', () => {
    const companyId = 1;
    const errorMessage = 'Server Error';

    service.getOrdersForCompany(companyId).subscribe(
      () => {},
      (error: HttpErrorResponse) => {
        expect(error.error.message).toBe(errorMessage);
      }
    );

    const req = httpTestingController.expectOne(`/api/order/${companyId}`);
    req.flush(errorMessage, { status: 500, statusText: 'Server Error' });
  });
});
