import { Filter } from "./filter";

export class LessThanFilter extends Filter {
    toString(): string {
        return `${this.filterField} lt ${this.filterValue}`;
    }
}