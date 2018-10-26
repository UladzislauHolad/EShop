import { Filter } from "./filter";

export class ContainsFilter extends Filter {
    public toString(): string {
        return `contains(tolower(${this.filterField}),'${this.filterValue.toLowerCase()}')`;
    }
}