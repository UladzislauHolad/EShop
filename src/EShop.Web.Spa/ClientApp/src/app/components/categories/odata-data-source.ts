import { CollectionViewer, DataSource } from "@angular/cdk/collections";
import { Observable, BehaviorSubject, of, Subject } from "rxjs";
import { catchError, finalize, map } from "rxjs/operators";
import { OdataService } from "src/app/services/odata.service";
import { IPagingService } from "src/app/services/IPagingService";
import { TableData } from "src/app/models/table-data";
import { Filter } from "src/app/helpers/filters/filter";

export class OdataDataSource<T> implements DataSource<T> {

    private dataSubject = new BehaviorSubject<T[]>([]);
    private loadingSubject = new BehaviorSubject<boolean>(false);
    private totalSubject = new Subject<number>();
    private odataService: OdataService<T[]>;

    public loading$ = this.loadingSubject.asObservable();
    public total$ = this.totalSubject.asObservable();

    constructor(private pagingService: IPagingService<T[]>) {
        this.odataService = new OdataService<T[]>(this.pagingService);
    }


    connect(collectionViewer: CollectionViewer): Observable<T[]> {
        return this.dataSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
        this.dataSubject.complete();
        this.loadingSubject.complete();
    }

    loadData(
        filters: Filter[],
        pageIndex: number,
        pageSize: number, 
        sortField: string, 
        sortDirection: string) {

        this.loadingSubject.next(true);

        this.odataService.getPaggedData(
            filters,
            pageIndex,
            pageSize,
            sortField, 
            sortDirection)
            .pipe(
                map(odata => {
                    let data = ( odata as any ).value;
                    let total = parseInt( odata[ '@odata.count' ]);
                    return new TableData<T>( data, total );
                }),
                catchError(() => of([])),
                finalize(() => this.loadingSubject.next(false))
            ).subscribe(tableData => {
                this.totalSubject.next((tableData as TableData<T>).total)
                this.dataSubject.next((tableData as TableData<T>).data);
            });
    }
}