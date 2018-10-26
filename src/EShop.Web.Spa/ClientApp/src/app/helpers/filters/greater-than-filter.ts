import { Filter } from "./filter";

export class GreaterThanFilter extends Filter {
    toString(): string {
        return `${this.filterField} gt ${this.filterValue}`;
    }
}