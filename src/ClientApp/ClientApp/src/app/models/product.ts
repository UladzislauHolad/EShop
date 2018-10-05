import { Category } from "./category";

export class Product {
    productId: number;
    name: string;
    price: number;
    description: string;
    count: number;
    categories: Category[];
}