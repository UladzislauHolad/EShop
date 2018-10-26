export abstract class  Filter {
    filterField: string;
    filterValue: string;

    constructor(filterField: string, filterValue: string) {
        this.filterField = filterField;
        this.filterValue = filterValue;
    }

    abstract toString(): string;
}