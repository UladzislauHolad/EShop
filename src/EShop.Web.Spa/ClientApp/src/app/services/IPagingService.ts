import { HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";

export interface IPagingService<T> {
    getPaggedData(params: HttpParams) : Observable<T>;
}