import { OrderProductModel } from "./orderProduct.model";

export interface OrderModel {
    orderId: number;
    description: string;
    orderTotal: number;
    products: OrderProductModel[];
}