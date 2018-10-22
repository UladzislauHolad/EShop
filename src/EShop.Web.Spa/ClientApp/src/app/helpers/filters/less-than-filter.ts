import { Filter } from "./filter";

export class LessThanFilter implements Filter {
    filterField: string;
    filterValue: string;

    constructor(filterField: string, filterValue: string)
    {
        this.filterField = filterField;
        this.filterValue = filterValue;
    }

    toString(): string {
        return `${this.filterField} lt ${this.filterValue}`;
    }
}