import { IFilter } from "./ifilter";

export class AnyFilter implements IFilter{
    expandField: string;
    filter: IFilter;

    constructor(filter: IFilter, expandField:string){
        this.filter = filter;
        this.expandField = expandField;
    }

    toString(): string {
        return `${this.expandField}/any(it:it/${this.filter.toString()})`;
    }
}