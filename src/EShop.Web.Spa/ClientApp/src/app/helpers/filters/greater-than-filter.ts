import { Filter } from "./filter";
import { IFilter } from "./ifilter";

export class GreaterThanFilter extends Filter implements IFilter{
    toString(): string {
        return `${this.filterField} gt ${this.filterValue}`;
    }
}