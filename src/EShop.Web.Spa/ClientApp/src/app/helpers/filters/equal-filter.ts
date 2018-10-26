import { Filter } from "./filter";
import { IFilter } from "./ifilter";

export class EqualFilter extends Filter implements IFilter{
    toString(): string {
        return `${this.filterField} eq ${this.filterValue.toLowerCase()}`;
    }
}