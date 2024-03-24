import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'web-app-orders-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './orders-list.component.html',
  styleUrl: './orders-list.component.css',
})
export class OrdersListComponent {
  orders: any[] = [];

  constructor(http: HttpClient) {
    // todo this should be in a separate service
    // also handle errors and display user an appropriate message
    http.get<any>('/api/order/1').subscribe((orders) => {
      this.orders = orders;
    });    
  }

  get hasOrders() : boolean{
    return this.orders && this.orders.length > 0;
  }
}
