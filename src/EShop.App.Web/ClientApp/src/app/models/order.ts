import { Customer } from "./customer";
import { PaymentMethod } from "./paymentMethod";
import { DeliveryMethod } from "./deliveryMethod";
import { PickupPoint } from "./pickupPoint";
import { ProductOrder } from "./productOrder";

export class Order {
    orderId: number;
    date: Date;
    status: string;
    comment: string;
    customerId: number;
    customer: Customer;
    paymentMethodId: number;
    paymentMethodName: string;
    paymentMethod: PaymentMethod;
    deliveryMethodId: number;
    deliveryMethodName: string;
    deliveryMethod: DeliveryMethod;
    pickupPointId: number;
    pickupPoint: PickupPoint;
    productOrders: ProductOrder[];
}