import { Type } from "@angular/core";

export class State {
    constructor(public component: Type<any>, public order: any) {}
}