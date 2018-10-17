import { CollectionViewer, DataSource } from "@angular/cdk/collections";
import { Category } from "src/app/models/category";
import { Observable, BehaviorSubject, of } from "rxjs";
import { CategoryService } from "src/app/services/category.service";
import { catchError, finalize } from "rxjs/operators";

export class CategoriesDataSource implements DataSource<Category> {

    private categoriesSubject = new BehaviorSubject<Category[]>([]);
    private loadingSubject = new BehaviorSubject<boolean>(false);

    public loading$ = this.loadingSubject.asObservable();

    constructor(private categoryService: CategoryService) { }


    connect(collectionViewer: CollectionViewer): Observable<Category[]> {
        console.log('connect');
        return this.categoriesSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
        console.log('disconnect');
        this.categoriesSubject.complete();
        this.loadingSubject.complete();
    }

    loadCategories(pageSize: number) {
        console.log('load');
        this.loadingSubject.next(true);

        this.categoryService.getOdataCategories(pageSize)   
            .pipe(
                catchError(() => of([])),
                finalize(() => this.loadingSubject.next(false))
            ).subscribe(categories => {
                console.dir(categories);
                this.categoriesSubject.next(categories);
            });

        // this.categoryService.findLessons(courseId, filter, sortDirection,
        //     pageIndex, pageSize).pipe(
        //         catchError(() => of([])),
        //         finalize(() => this.loadingSubject.next(false))
        //     )
        // .subscribe(lessons => this.lessonsSubject.next(lessons));
    }
}