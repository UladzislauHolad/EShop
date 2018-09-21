import { Product } from "./product";

export class ProductOrder {
    productOrderId: number;
    orderId: number;
    productId: number;
    orderCount: number;
    name: string;
    price: number;
    isEditing: boolean = false;
    availableCount: number;
}