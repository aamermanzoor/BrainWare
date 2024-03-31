import { Component, OnDestroy, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrderService } from 'src/app/services/order.service';
import { OrderModel } from 'src/app/models/order.model';
import { Subscription } from 'rxjs';

@Component({
  selector: 'web-app-orders-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './orders-list.component.html',
  styleUrl: './orders-list.component.css',
})
export class OrdersListComponent implements OnInit, OnDestroy {
  companyId : number = 1;
  isLoading : boolean = false;
  hasError : boolean = false;
  orders: OrderModel[] = [];
  orderSubscription: Subscription = new Subscription();

  constructor(private readonly orderService : OrderService) {    
  }

  ngOnInit(): void {
    this.loadOrders();
  }

  loadOrders(): void {
    this.isLoading = true;
    this.orderSubscription = this.orderService.getOrdersForCompany(this.companyId).subscribe({
      next: (data: OrderModel[]) => {        
        this.orders = data;        
        this.isLoading = false;
      },
      error: () => {
        this.hasError = true;
        this.isLoading = false;
      }
    });
  }

  ngOnDestroy(): void {
    if (this.orderSubscription) {
      this.orderSubscription.unsubscribe();
    }
  }

  get showOrders() : boolean {
    return !this.hasError && !this.isLoading && this.orders && this.orders.length > 0;
  }

  get showNoOrdersMessage() : boolean {
    return !this.hasError && !this.isLoading && (!this.orders || this.orders.length === 0);
  }
}
