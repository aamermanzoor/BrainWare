import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { OrdersListComponent } from './components/orders-list/orders-list.component';

@Component({
  standalone: true,
  imports: [CommonModule, RouterModule, OrdersListComponent],
  selector: 'web-app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  year = new Date().getFullYear();
}
