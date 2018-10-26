import { Filter } from "./filter";
import { IFilter } from "./ifilter";

export class ContainsFilter extends Filter implements IFilter {
    public toString(): string {
        return `contains(tolower(${this.filterField}),'${this.filterValue.toLowerCase()}')`;
    }
}