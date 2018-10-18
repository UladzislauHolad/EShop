import { CollectionViewer, DataSource } from "@angular/cdk/collections";
import { Observable, BehaviorSubject, of } from "rxjs";
import { catchError, finalize } from "rxjs/operators";
import { OdataService } from "src/app/services/odata.service";
import { IPagingService } from "src/app/services/IPagingService";

export class OdataDataSource<T> implements DataSource<T> {

    private dataSubject = new BehaviorSubject<T[]>([]);
    private loadingSubject = new BehaviorSubject<boolean>(false);
    private odataService: OdataService<T[]>;

    public loading$ = this.loadingSubject.asObservable();

    constructor(private pagingService: IPagingService<T[]>) {
        this.odataService = new OdataService<T[]>(this.pagingService);
    }


    connect(collectionViewer: CollectionViewer): Observable<T[]> {
        console.log('connect');
        return this.dataSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
        console.log('disconnect');
        this.dataSubject.complete();
        this.loadingSubject.complete();
    }

    loadCategories(pageSize: number) {
        console.log('load');
        this.loadingSubject.next(true);

        this.odataService.getPaggedData()
            .pipe(
                catchError(() => of([])),
                finalize(() => this.loadingSubject.next(false))
            ).subscribe(data => {
                console.dir(data);
                this.dataSubject.next(data);
            });
    }
}