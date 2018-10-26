import { Filter } from "./filter";
import { IFilter } from "./ifilter";

export class LessThanFilter extends Filter implements IFilter {
    toString(): string {
        return `${this.filterField} lt ${this.filterValue}`;
    }
}