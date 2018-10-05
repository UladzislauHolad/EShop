import { Injectable } from "@angular/core";
import { Subject } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class SideNavEventService {
    private onChooseSource = new Subject<number>();

    onChoose$ = this.onChooseSource.asObservable();

    choose(id: number) {
        this.onChooseSource.next(id);
    }
}