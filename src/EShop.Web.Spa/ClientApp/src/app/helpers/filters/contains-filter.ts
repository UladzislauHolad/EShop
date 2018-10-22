import { Filter } from "./filter";

export class ContainsFilter implements Filter {
    filterField: string;
    filterValue: string;

    constructor(filterField: string, filterValue: string) {
        this.filterField = filterField;
        this.filterValue = filterValue;
    }
    
    public toString(): string {
        return `contains(tolower(${this.filterField}),'${this.filterValue}')`;
    }
}