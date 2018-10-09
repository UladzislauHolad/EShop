import { Category } from "../../../models/category";

export class CategoryNestedNode {
    category: Category;
    childs: CategoryNestedNode[];
}